using Caliburn.Micro;
using CefSharp.Wpf;

namespace hello.webapi.wpf
{
    public class BrowserViewModel : Screen
    {
        private IWpfWebBrowser _webBrowser;

        public IWpfWebBrowser WebBrowser
        {
            get { return _webBrowser; }
            set { _webBrowser = value; NotifyOfPropertyChange(); }
        }
    }
}