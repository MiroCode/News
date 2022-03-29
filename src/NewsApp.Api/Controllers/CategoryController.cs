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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="categoryManager"></param>
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListCategoryQueryRequest requestModel)
        {

            var result = await _categoryManager.GetAllCategoryAsync(requestModel);
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
            var requestModel = new GetCategoryQueryRequest
            {
                Id = id
            };

            var result = await _categoryManager.GetCategoryAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommandRequest requestModel)
        {
            var result = await _categoryManager.CreateCategoryAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommandRequest requestModel)
        {
            var result = await _categoryManager.UpdateCategoryAsync(requestModel);
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
            var requestModel = new DeleteCategoryCommandRequest
            {
                Id = id
            };

            var result = await _categoryManager.DeleteCategoryAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }

    }
}
