using System;
using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Views
{
    public class ShellViewModel : Screen, IShell
    {
        private readonly IMessageService _messageService;

        public ShellViewModel(IEventAggregator events, ISalesViewModel salesViewModel, IMessageService messageService)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (salesViewModel == null) throw new ArgumentNullException(nameof(salesViewModel));
            if (messageService == null) throw new ArgumentNullException(nameof(messageService));

            events.Subscribe(this);
            Sales = salesViewModel;
            _messageService = messageService;

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            DisplayName = "Hello.WebAPI.WPF";
        }

        public ISalesViewModel Sales { get; }

        public void Handle(MessageEvent message)
        {
            message.Result = _messageService.Show(
                message.Message, message.Caption, message.Button, message.Icon);
        }
    }
}