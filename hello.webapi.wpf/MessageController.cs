using System;
using System.Threading.Tasks;
using System.Web.Http;
using Caliburn.Micro;

namespace hello.webapi.wpf
{
    public class MessageController : ApiController
    {
        private readonly IEventAggregator _events;

        protected MessageController(IEventAggregator events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            _events = events;
        }

        public async Task<IHttpActionResult> Get(string message)
        {
            return await Post(FormatMessage(message));
        }

        public async Task<IHttpActionResult> Post(MessageEvent message)
        {
            if (message == null) return BadRequest("Could not deserialize message from request body");

            await _events.PublishOnUIThreadAsync(message);

            if (message.Result == MessageResult.Yes || message.Result == MessageResult.Ok)
                return Ok();

            return Unauthorized();
        }

        protected virtual MessageEvent FormatMessage(string message)
        {
            return new MessageEvent {Message = message};
        }
    }

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