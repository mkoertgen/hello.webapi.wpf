using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace hello.webapi.wpf
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        //readonly HubConnection _hubConnection;
        //readonly IHubProxy _mainHubProxy;

        public BrowserViewModel Browser { get; private set; }

        public ShellViewModel()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            DisplayName = "Hello.WebAPI.WPF";

            Browser = new BrowserViewModel();

            //NavigationWebBrowser.Source = new Uri("http://localhost:9000/web/default.html");

            //_hubConnection = new HubConnection(AppBootstrapper.baseWebSignalRAddress);
            //_mainHubProxy = _hubConnection.CreateHubProxy("MainHub");
            //    _mainHubProxy.On<string, string>("addMessage", (invoker, data) =>
            //    {
            //        if (invoker == "htmlbutton" && data == "blue")
            //            Dispatcher.InvokeAsync(() => LoadContent.Background = Brushes.Blue);
            //    });
            //    _hubConnection.Start();
        }

        public void DoLoadContent()
        {
            //_mainHubProxy.Invoke("Send", "button", "getdata");
        }
    }
}