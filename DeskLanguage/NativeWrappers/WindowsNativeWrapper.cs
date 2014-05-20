using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeskLanguage.NativeWrappers
{
    public class WindowsNativeWrapper : INativeWrapper
    {
        private const uint GW_HWNDNEXT = 2;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr sibling, uint flags);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr window, StringBuilder result, int maxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr window);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr window);

        private static string GetWindowText(IntPtr window)
        {
            int length = GetWindowTextLength(window);
            StringBuilder result = new StringBuilder(length + 1);
            return (GetWindowText(window, result, result.Capacity) > 0) ? result.ToString() : "";
        }

        public IEnumerable<Models.Window> ListWindows()
        {
            List<Models.Window> windows = new List<Models.Window>();
            IntPtr window = FindWindow(null, null);
            while(window != IntPtr.Zero)
            {
                yield return new Models.Window
                {
                    WindowName = GetWindowText(window),
                    IsVisible = IsWindowVisible(window)
                };
                window = GetWindow(window, GW_HWNDNEXT);
            }
        }
    }
}
