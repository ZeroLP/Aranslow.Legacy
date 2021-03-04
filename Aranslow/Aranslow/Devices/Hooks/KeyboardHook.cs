using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Aranslow.Tools;

namespace Aranslow.Devices.Hooks
{
    internal class KeyboardHook
    {
        private static IntPtr WKBHookInstance = IntPtr.Zero;

        private static NativeImport.WindowsHookAdditionals.HookProc WinKBHookCallbackDelegate = null;

        public static void InstallHook()
        {
            if(WinKBHookCallbackDelegate == null && WKBHookInstance == IntPtr.Zero)
            {
                WinKBHookCallbackDelegate = new NativeImport.WindowsHookAdditionals.HookProc(HookedKBWindowsCallback);

                var hinstance = NativeImport.LoadLibrary("User32");

                WKBHookInstance = NativeImport.SetWindowsHookEx(NativeImport.WindowsHookAdditionals.HookType.WH_KEYBOARD_LL, WinKBHookCallbackDelegate, hinstance, 0);

                Logger.Log("Installed Keyboard hook.");
            }
        }

        public static void UninstallHook()
        {
            if(WKBHookInstance != null)
            {
                NativeImport.UnhookWindowsHookEx(WKBHookInstance);
                Logger.Log("Uninstalled Keyboard hook.");
            }
        }

        public delegate void OnKeyPressCallback(Key keyBeingPressed, string pressState);

        #region Events
        public static event OnKeyPressCallback OnKeyPress;
        #endregion

        private static IntPtr HookedKBWindowsCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                try
                {
                    OnKeyPress(KeyInterop.KeyFromVirtualKey(Marshal.ReadInt32(lParam)), wParam.ToInt32() == NativeImport.WindowsMessagesModifiers.WM_KEYDOWN ? "Down" : "Up");
                }
                catch { }

                return NativeImport.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }
            else
            {
                return NativeImport.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }
        }

        public class KeyPressedArgs : EventArgs
        {
            public Key KeyPressed { get; private set; }

            public KeyPressedArgs(Key key)
            {
                KeyPressed = key;
            }
        }
    }
}
