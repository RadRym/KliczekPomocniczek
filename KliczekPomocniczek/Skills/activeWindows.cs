﻿using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
                if(process.ProcessName == NameProc)
                    n++;
            });
            if (n > 0)
                return true;
            return false;
        }
    }
}
