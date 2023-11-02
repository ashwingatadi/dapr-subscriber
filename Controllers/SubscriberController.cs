using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Subscriber.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController : ControllerBase
    {
        ILogger<SubscriberController> logger;

        public SubscriberController(ILogger<SubscriberController> logger)
        {
            this.logger = logger;
        }

        [Topic("myapp-pubsub", "test-dapr-topic")]
        [HttpPost("subscriber")]
        public IActionResult Post([FromBody] string value)
        {
            logger.LogInformation(value);
            return Ok(value);
        }
    }
}
