using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Controllers
{
    public class WarningController : MessageController
    {
        public WarningController(IEventAggregator events) : base(events)
        {
        }

        protected override MessageEvent FormatMessage(string message)
        {
            return MessageEvent.Warning(message);
        }
    }
}