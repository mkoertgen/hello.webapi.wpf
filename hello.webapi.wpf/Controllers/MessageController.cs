using System;
using System.Threading.Tasks;
using System.Web.Http;
using Caliburn.Micro;
using hello.webapi.wpf.Models;

namespace hello.webapi.wpf.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IEventAggregator _events;

        protected MessageController(IEventAggregator events)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));
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
}