using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsImageController : ControllerBase
    {
        private readonly INewsImageManager _newsImageManager;
        public NewsImageController(INewsImageManager newsImage)
        {
            _newsImageManager = newsImage;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListNewsImageQueryRequest requestModel)
        {
            var result = await _newsImageManager.GetAllNewsImageAsync(requestModel);
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
            var requestModel = new GetNewsImageQueryRequest
            {
                Id = id
            };

            var result = await _newsImageManager.GetNewsImageAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateNewsImageCommandRequest requestModel)
        {
            var result = await _newsImageManager.CreateNewsImageAsync(requestModel);
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
        public async Task<IActionResult> Put([FromBody] UpdateNewsImageCommandRequest requestModel)
        {
            var result = await _newsImageManager.UpdateNewsImageAsync(requestModel);
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
            var requestModel = new DeleteNewsImageCommandRequest
            {
                Id = id
            };

            var result = await _newsImageManager.DeleteNewsImageAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
