using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MouseTrigger.Properties;

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
        static Label _lbl = null;
        static ComboBox _cbx = null;
        public void Init(Label lbl, ComboBox cbx)
        {
            _lbl = lbl;
            _cbx = cbx;
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
            keyHookId = HotKeyService.RegisterHotKey(Keys.F12, KeyModifiers.Control|KeyModifiers.NoRepeat);
            HotKeyService.HotKeyPressed += new EventHandler<KeyEventArgs>(HotKeyService_HotKeyPressed);
        }

        void HotKeyService_HotKeyPressed(object sender, KeyEventArgs e)
        {
            uint x = 0;
            if (SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref x, 0))
            {

                string cb_txt = ".5";
                _cbx.Invoke((MethodInvoker)delegate
                {
                    
                    
                    cb_txt = _cbx.Text;

                    
                });

                if (cb_txt == "")
                {
                    _lbl.Invoke((MethodInvoker)delegate {
                        _lbl.Text = "Ivalid Slowdown";
                    });
                   
                    return;
                }
                try
                {
                    float temp = float.Parse(cb_txt);
                    if (temp > 1.0)
                    {
                        _lbl.Invoke((MethodInvoker)delegate {
                            _lbl.Text = "Ivalid Slowdown";
                        });

                        return;
                    }
                }
                catch
                {
                    _lbl.Invoke((MethodInvoker)delegate {
                        _lbl.Text = "Ivalid Slowdown";
                    });

                    return;
                }
                float cb_val = float.Parse(cb_txt);

                SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)(orig * cb_val), 0);
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

                Settings.Default["Slowdown"] = cb_txt;
                Settings.Default.Save(); // Saves settings in application configuration file


            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        private static bool CheckUp()
        {
            int val = GetKeyState((int)Keys.F12) & 0x8000;
            System.Threading.Thread.Sleep(200);
            _lbl.Invoke((MethodInvoker)delegate {
                _lbl.Text = "Hot Key Down: " + GetKeyState((int)Keys.F12).ToString();
            });
            Debug.Print(GetKeyState((int)Keys.F12).ToString());
            return (val!=0);
        }
     
    }
}