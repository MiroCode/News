using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    /// <summary>
    /// NewsController
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsManager _newsManager;
        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="newsManager"></param>
        public NewsController(INewsManager newsManager)
        {
            _newsManager = newsManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListNewsQueryRequest requestModel)
        {
            var result = await _newsManager.GetAllNewsAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("AllNewsView")]
        public async Task<IActionResult> GetAllNewsView([FromQuery] ListNewsViewQueryRequest requestModel)
        {
            var result = await _newsManager.GetAllNewsViewAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("AllNewsCommentNPoint")]
        public async Task<IActionResult> GetAllNewsCommentNPoint([FromQuery] ListNewsCommentNPointQueryRequest request)
        {
            var result = await _newsManager.GetAllNewsCommentNPointAsync(request);
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
            var requestModel = new GetNewsQueryRequest
            {
                Id = id
            };

            var result = await _newsManager.GetNewsAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("NewsCategoryAll")]
        public async Task<IActionResult> ListRelation([FromBody] ListNewsQueryRequest request)
        {
            var result = await _newsManager.GetAllNewsAsync(request);
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("NewNews")]
        public async Task<IActionResult> Post([FromBody] CreateNewsCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _newsManager.CreateNewsAsync(requestModel);
            if (result == null)
                return NotFound();

            return StatusCode(201, result);
        }

        [HttpPost("newsView")]
        public async Task<IActionResult> CreateNewsView([FromBody] CreateNewsViewCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _newsManager.CreateNewsViewAsync(requestModel);
            if (result == null)
                return NotFound();

            return StatusCode(201, result);
        }

        [HttpPost("newsCommentNPoint")]
        public async Task<IActionResult> CreateNewsCommentNPoint([FromBody] CreateNewsCommentNPointCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _newsManager.CreateNewsCommentNPointAsync(requestModel);
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
        public async Task<IActionResult> Put([FromBody] UpdateNewsCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _newsManager.UpdateNewsAsync(requestModel);
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
            var requestModel = new DeleteNewsCommandRequest
            {
                Id = id
            };

            var result = await _newsManager.DeleteNewsAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
