using System.Web.Http;
using System.Web.Http.Dependencies;
using Owin;

namespace hello.webapi.wpf
{
    public class OwinWebApiConfig
    {
        public static IDependencyResolver DependencyResolver { get; set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // we don't want no XML, just JSON.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            if (DependencyResolver != null)
                config.DependencyResolver = DependencyResolver;

            appBuilder.UseWebApi(config);
        }
    }
}