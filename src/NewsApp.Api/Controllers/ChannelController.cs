using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    /// <summary>
    /// Manage search for booker To and From component 
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelManager _channelManager;
        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="channelManager"></param>
        public ChannelController(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListChannelQueryRequest requestModel)
        {

            var result = await _channelManager.GetAllChannelAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var requestModel = new GetChannelQueryRequest
            {
                Id = id
            };

            var result = await _channelManager.GetChannelAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChannelCommandRequest requestModel)
        {
            var result = await _channelManager.CreateChannelAsync(requestModel);
            if (result == null)
                return NotFound();

            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateChannelCommandRequest requestModel)
        {
            var result = await _channelManager.UpdateChannelAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var requestModel = new DeleteChannelCommandRequest
            {
                Id = id
            };

            var result = await _channelManager.DeleteChannelAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
