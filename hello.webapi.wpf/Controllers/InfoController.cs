using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Controllers
{
    public class InfoController : MessageController
    {
        public InfoController(IEventAggregator events) : base(events)
        {
        }

        protected override MessageEvent FormatMessage(string message)
        {
            return MessageEvent.Info(message);
        }
    }
}