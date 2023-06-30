using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseTrigger
{

    public partial class MonitorScreen : Form
    {
        [Flags]
        public enum SPIF
        {
            None = 0x00,
            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,
            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,
            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(long uiAction, uint uiParam, ref uint pvParam, SPIF fWinIni);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(long uiAction, uint uiParam, uint pvParam, SPIF fWinIni);

        public const uint SPI_GETMOUSESPEED = 0x0070;
        public const uint SPI_SETMOUSESPEED = 0x0071;



        public MonitorScreen()
        {
            InitializeComponent();

        }
        private void MonitorScreen_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                // notifyIcon1.Visible = true;
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            // notifyIcon1.Visible = false;
        }
        private void MonitorScreen_Load(object sender, EventArgs e)
        {
            new KeyClient().Init(label1);
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint x = 0;
            if (SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref x, 0))
            {
                uint t = x + 5;
                SystemParametersInfo(SPI_SETMOUSESPEED, 0, t, 0);
            }
        }
    }
}