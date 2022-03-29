using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChannelCategoryMapController : ControllerBase
    {
        private readonly IChannelCategoryMapManager _channelcategorymapManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="categoryManager"></param>
        public ChannelCategoryMapController(IChannelCategoryMapManager channelcategorymapManager)
        {
            _channelcategorymapManager = channelcategorymapManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListChannelCategoryMapQueryRequest requestModel)
        {

            var result = await _channelcategorymapManager.GetAllChannelCategoryMapAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var requestModel = new GetChannelCategoryMapQueryRequest
            {
                Id = id
            };
            var result = await _channelcategorymapManager.GetChannelCategoryMapAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateChannelCategoryMapCommandRequest requestModel)
        {
            var result = await _channelcategorymapManager.CreateChannelCategoryMapAsync(requestModel);
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
        public async Task<IActionResult> Put([FromBody] UpdateChannelCategoryMapCommandRequest requestModel)
        {
            var result = await _channelcategorymapManager.UpdateChannelCategoryMapAsync(requestModel);
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
            var requestModel = new DeleteChannelCategoryMapCommandRequest
            {
                Id = id
            };
            var result = await _channelcategorymapManager.DeleteChannelCategoryMapAsync(requestModel);
            if (result == null)
                return NotFound();
            return Ok();
        }
    }
}
