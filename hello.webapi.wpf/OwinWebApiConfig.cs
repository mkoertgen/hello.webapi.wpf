using System.Web.Http;
using Owin;

namespace hello.webapi.wpf
{
    public class OwinWebApiConfig
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseStaticFiles("/web");

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            appBuilder.UseWebApi(config);
        }
    }
}