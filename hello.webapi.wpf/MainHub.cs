using Microsoft.AspNet.SignalR;

namespace hello.webapi.wpf
{
    public class MainHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}