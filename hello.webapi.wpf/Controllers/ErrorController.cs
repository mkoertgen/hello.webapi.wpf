using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Controllers
{
    public class ErrorController : MessageController
    {
        public ErrorController(IEventAggregator events) : base(events)
        {
        }

        protected override MessageEvent FormatMessage(string message)
        {
            return MessageEvent.Error(message);
        }
    }
}