using Microsoft.Owin.Cors;
using Owin;

namespace hello.webapi.wpf
{
    public class OwinSignalRConfig
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();
        }
    }
}