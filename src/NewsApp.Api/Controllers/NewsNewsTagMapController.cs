using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsNewsTagMapController : ControllerBase
    {
        private readonly INewsNewsTagMapManager _newsnewstagmapManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="newsnewstagmapManager"></param>
        public NewsNewsTagMapController(INewsNewsTagMapManager newsnewstagmapManager)
        {
            _newsnewstagmapManager = newsnewstagmapManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListNewsNewsTagMapQueryRequest requestModel)
        {

            var result = await _newsnewstagmapManager.GetAllNewsNewsTagMapAsync(requestModel);
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
            var requestModel = new GetNewsNewsTagMapQueryRequest
            {
                Id = id
            };

            var result = await _newsnewstagmapManager.GetNewsNewsTagMapAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateNewsNewsTagMapCommandRequest requestModel)
        {
            var result = await _newsnewstagmapManager.CreateNewsNewsTagMapAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateNewsNewsTagMapCommandRequest requestModel)
        {
            var result = await _newsnewstagmapManager.UpdateNewsNewsTagMapAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var requestModel = new DeleteNewsNewsTagMapCommandRequest
            {
                Id = id
            };

            var result = await _newsnewstagmapManager.DeleteNewsNewsTagMapAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
