using System.Windows;

namespace hello.webapi.wpf
{
    internal static class WindowExtensions
    {
        public static void BringToFront(this Window window, WindowState windowState = WindowState.Normal)
        {
            // cf.: https://blog.binarybits.net/programming/bringing-window-to-front-in-wpf/
            if (!window.IsVisible)
            {
                window.Show();
            }
            window.WindowState = windowState;
            window.Activate();
            window.Topmost = true;
            window.Topmost = false;
            window.Focus();
        }
    }
}
