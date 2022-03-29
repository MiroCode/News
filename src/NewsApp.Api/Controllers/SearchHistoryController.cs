using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchHistoryController : ControllerBase
    {
        private readonly ISearchHistoryManager _searchHistoryManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="searchHistoryManager"></param>
        public SearchHistoryController(ISearchHistoryManager searchHistoryManager)
        {
            _searchHistoryManager = searchHistoryManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListSearchHistoryQueryRequest requestModel)
        {

            var result = await _searchHistoryManager.GetAllSearchHistoryAsync(requestModel);
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
            var requestModel = new GetSearchHistoryQueryRequest
            {
                Id = id
            };

            var result = await _searchHistoryManager.GetSearchHistoryAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateSearchHistoryCommandRequest requestModel)
        {
            var result = await _searchHistoryManager.CreateSearchHistoryAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateSearchHistoryCommandRequest requestModel)
        {
            var result = await _searchHistoryManager.UpdateSearchHistoryAsync(requestModel);
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
            var requestModel = new DeleteSearchHistoryCommandRequest
            {
                Id = id
            };

            var result = await _searchHistoryManager.DeleteSearchHistoryAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
