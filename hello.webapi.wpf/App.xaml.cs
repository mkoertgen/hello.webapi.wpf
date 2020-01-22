using System;
using System.Collections.Generic;
using System.Windows;

namespace hello.webapi.wpf
{
    public partial class App : ISingleInstanceApp
    {
        public void SignalExternalCommandLineArgs(IList<string> args)
        {
            OnUiThread(() =>
            {
                MainWindow.BringToFront();
                var msg = $"Called with args='{string.Join(" ", args)}'";
                MessageBox.Show(MainWindow, msg, "Information", MessageBoxButton.OK);
            });
        }

        private static void OnUiThread(Action action)
        {
            if (!Current.CheckAccess())
                Current.Dispatcher?.Invoke(action);
            else
                action();
        }
    }
}
