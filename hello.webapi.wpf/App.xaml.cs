using System.Collections.Generic;
using System.Windows;

namespace hello.webapi.wpf
{
    public partial class App : ISingleInstanceApp
    {
        public void SignalExternalCommandLineArgs(IList<string> args)
        {
            var msg = $"Called with args='{string.Join(" ", args)}'";
            MessageBox.Show(msg, "Information", MessageBoxButton.OK);
        }
    }
}