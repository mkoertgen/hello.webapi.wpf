using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Integration.WebApi;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using hello.webapi.wpf.Models;
using hello.webapi.wpf.Views;
using Microsoft.Owin.Hosting;

namespace hello.webapi.wpf
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageService>().As<IMessageService>().SingleInstance();

            builder.RegisterType<SalesViewModel>().AsImplementedInterfaces().SingleInstance();

            // cf.: http://autofac.readthedocs.org/en/latest/integration/webapi.html#register-controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            OwinWebApiConfig.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            WebApp.Start<OwinWebApiConfig>("http://localhost:9000/");
        }
    }
}