using System;
using System.Collections.Generic;
using System.Web;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf
{
    public partial class App : ISingleInstanceApp
    {
        private AppBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper = new AppBootstrapper();
            base.OnStartup(e);
            SignalExternalCommandLineArgs(e.Args);
        }

        public void SignalExternalCommandLineArgs(IList<string> args)
        {
            var url = string.Join("/", args);
            if (!TryParse(url, out var message)) return;
            var events = _bootstrapper.ComponentContext.Resolve<IEventAggregator>();
            events.PublishOnUIThread(message);
        }

        private static bool TryParse(string url, out MessageEvent messageEvent)
        {
            messageEvent = MessageEvent.Error($"Could not parse: '{url}'.");
            if (string.IsNullOrWhiteSpace(url)) return false;
            // protocol://<info>/message
            var tokens = url.Split(new []{'/',':'},
                StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 3) return false;
            var caption = tokens[1];
            if (!Enum.TryParse<MessageImage>(caption, true, out var icon)) return false;
            var message = HttpUtility.UrlDecode(tokens[2]);
            messageEvent = new MessageEvent {Caption = icon.ToString(), Message = message, Icon = icon};
            return true;
        }
    }
}
