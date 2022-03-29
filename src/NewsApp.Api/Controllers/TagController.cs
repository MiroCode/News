using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    /// <summary>
    /// TagController
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagManager _tagManager;
        private readonly ILogger<TagController> _logger;

        /// <summary>
        /// TagController
        /// </summary>
        /// <param name="tagManager"></param>
        /// <param name="logger"></param>
        public TagController(ITagManager tagManager, ILogger<TagController> logger)
        {
            _tagManager = tagManager;
            _logger = logger;
        }

        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListTagQueryRequest requestModel)
        {
            var result = await _tagManager.GetAllTagAsync(requestModel);
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
            var requestModel = new GetTagQueryRequest
            {
                Id = id
            };

            var result = await _tagManager.GetTagAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateTagCommandRequest requestModel)
        {
            try
            {
                var result = await _tagManager.CreateTagAsync(requestModel);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTagCommandRequest requestModel)
        {

            var result = await _tagManager.UpdateTagAsync(requestModel);
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
            var requestModel = new DeleteTagCommandRequest
            {
                Id = id
            };

            var result = await _tagManager.DeleteTagAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }

    }
}

