using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Aranslow.Tools
{
    internal class NativeImport
    {
        internal class WindowsMessagesModifiers
        {
            internal const int WM_NCLBUTTONDOWN = 0xA1;
            internal const int HTCAPTION = 0x2;

            internal const int WM_KEYDOWN = 0x0100;
            internal const int WM_KEYUP = 0x0101;
        }

        internal class WindowsHookAdditionals
        {
            internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

            internal enum HookType : int
            {
                WH_JOURNALRECORD = 0,
                WH_JOURNALPLAYBACK = 1,
                WH_KEYBOARD = 2,
                WH_GETMESSAGE = 3,
                WH_CALLWNDPROC = 4,
                WH_CBT = 5,
                WH_SYSMSGFILTER = 6,
                WH_MOUSE = 7,
                WH_HARDWARE = 8,
                WH_DEBUG = 9,
                WH_SHELL = 10,
                WH_FOREGROUNDIDLE = 11,
                WH_CALLWNDPROCRET = 12,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14
            }
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(WindowsHookAdditionals.HookType hookType, WindowsHookAdditionals.HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        internal static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }
}
