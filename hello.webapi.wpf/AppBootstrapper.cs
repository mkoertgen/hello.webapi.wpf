using System.Windows;
using Autofac;
using Caliburn.Micro.Autofac;
using CefSharp;
using Microsoft.Owin.Hosting;

namespace hello.webapi.wpf
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public AppBootstrapper()
        {
            Cef.Initialize(new CefSettings());

            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {

        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }


        public static string baseWebAPIAddress = "http://localhost:9000/";
        public static string baseWebSignalRAddress = "http://localhost:8080/";

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            WebApp.Start<OwinWebApiConfig>(url: baseWebAPIAddress);
            WebApp.Start<OwinSignalRConfig>(url: baseWebSignalRAddress);
        }
    }
}