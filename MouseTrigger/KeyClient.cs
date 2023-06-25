using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseTrigger
{
    internal class KeyClient
    {

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(long uiAction, uint uiParam, ref uint pvParam, uint flag);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(long uiAction, uint uiParam, uint pvParam, uint flag);

        public const uint SPI_GETMOUSESPEED = 0x0070;
        public const uint SPI_SETMOUSESPEED = 0x0071;

        int keyHookId;
        uint orig = 0;
        Label _lbl = null;

        public void Init(Label lbl)
        {
            _lbl = lbl;
            uint x = 0;
            if (SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref x, 0))
            {
                orig = x;
                Setup();
            }

        }

        private void Cleanup()
        {
            HotKeyService.UnregisterHotKey(keyHookId);
        }

        private void Setup()
        {
            keyHookId = HotKeyService.RegisterHotKey(Keys.F11, KeyModifiers.Control|KeyModifiers.NoRepeat);
            HotKeyService.HotKeyPressed += new EventHandler<KeyEventArgs>(HotKeyService_HotKeyPressed);
        }

        void HotKeyService_HotKeyPressed(object sender, KeyEventArgs e)
        {
            uint x = 0;
            if (SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref x, 0))
            {
               
                SystemParametersInfo(SPI_SETMOUSESPEED, 0, orig/3, 0);
                Debug.Print("Pressed");
                _lbl.Invoke((MethodInvoker)delegate {
                    _lbl.Text = "Hot Key Down"; ;
                });
  
                while (CheckUp())
                {

                }
                SystemParametersInfo(SPI_SETMOUSESPEED, 0, (orig), 0);
                Debug.Print("Released");
                _lbl.Invoke((MethodInvoker)delegate {
                    _lbl.Text = "Hot Key Released";
                });

                
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        private static bool CheckUp()
        {
            int val = GetKeyState((int)Keys.F11) & 0x8000;
            System.Threading.Thread.Sleep(200);
            return (val!=0);
        }
     
    }
}