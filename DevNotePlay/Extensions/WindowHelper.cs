using System;
using System.Linq;
using System.Windows;

namespace Player.Extensions
{
    public static class WindowHelper
    {
        public static void OpenWindow<T>() where T : Window
        {
            var windows = Application.Current.Windows.Cast<Window>();

            var any = windows.Any(s => s is T);

            if (any)
            {
                var win = windows.Where(s => s is T).ToList()[0];

                if (win.WindowState == WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Focus();
            }
            else
            {
                object[] parameters = new object[1]
                {
                    null
                };
                var win = (Window)Activator.CreateInstance(typeof(T), parameters);
                win.Show();
            }
        }
    }
}
