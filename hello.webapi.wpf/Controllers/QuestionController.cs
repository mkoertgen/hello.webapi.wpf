using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Controllers
{
    public class QuestionController : MessageController
    {
        public QuestionController(IEventAggregator events) : base(events)
        {
        }

        protected override MessageEvent FormatMessage(string message)
        {
            return MessageEvent.Question(message);
        }
    }
}