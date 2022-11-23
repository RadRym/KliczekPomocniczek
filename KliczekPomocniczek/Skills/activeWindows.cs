using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using KliczekPomocniczek.QuickMenu;
using System.Windows.Forms;

namespace KliczekPomocniczek.Skills
{
    public class activeWindows
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);

        public static string ActiveWindowTitle()
        {
            const int nChar = 256;
            StringBuilder ss = new StringBuilder(nChar);

            IntPtr handle = IntPtr.Zero;
            handle = GetForegroundWindow();

            if (GetWindowText(handle, ss, nChar) > 0) return ss.ToString();
            else return "";
        }

        public static bool isActive(string NameProc)
        {
            int n = 0;
            Process[] processes = Process.GetProcesses();
            Array.ForEach(processes, (process) =>
            {
                if (process.ProcessName == NameProc)
                    n++;
            });
            if (n > 0)
                return true;
            return false;
        }

        public static void RunQuickMenu(QuickMenuPage QuickMenuPage)
        {
            double WidthBig;
            double WidthSmall;
            double HeightBig;
            double HeightSmall;

            int Width1 = Screen.AllScreens[0].WorkingArea.Width;
            int Width2 = Screen.AllScreens[1].WorkingArea.Width;
            int Height1 = Screen.AllScreens[0].WorkingArea.Width;
            int Height2 = Screen.AllScreens[1].WorkingArea.Width;
            if (Width1 > Width2)
            {
                WidthBig = Width1;
                WidthSmall = Width2;
                HeightBig = Height1;
                HeightSmall = Height2;
            }
            else
            {
                WidthBig = Width2;
                WidthSmall = Width1;
                HeightBig = Height2;
                HeightSmall = Height1;
            }

            if (QuickMenuPage.IsActive == true) return;
            else
            {
                if(Screen.AllScreens.Length != 2)
                {
                    QuickMenuPage.Left = System.Windows.Forms.Control.MousePosition.X - 250;
                    QuickMenuPage.Top = System.Windows.Forms.Control.MousePosition.Y - 250;
                    QuickMenuPage.Topmost = true;
                    QuickMenuPage.Show();
                }
                else if(Screen.AllScreens.Length == 2)
                {
                    QuickMenuPage.Left = WidthSmall / WidthBig * System.Windows.Forms.Control.MousePosition.X - 250;
                    QuickMenuPage.Top = HeightSmall / HeightBig * System.Windows.Forms.Control.MousePosition.Y - 250;
                    QuickMenuPage.Topmost = true;
                    QuickMenuPage.Show();
                }

            }
        }
    }
}
