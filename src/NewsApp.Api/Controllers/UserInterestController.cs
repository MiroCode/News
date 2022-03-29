using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserInterestController : ControllerBase
    {
        private readonly IUserInterestManager _userInterestManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="userInterestManager"></param>
        public UserInterestController(IUserInterestManager userInterestManager)
        {
            _userInterestManager = userInterestManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListUserInterestQueryRequest requestModel)
        {

            var result = await _userInterestManager.GetAllUserInterestAsync(requestModel);
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
            var requestModel = new GetUserInterestQueryRequest
            {
                Id = id
            };

            var result = await _userInterestManager.GetUserInterestAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateUserInterestCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _userInterestManager.CreateUserInterestAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserInterestCommandRequest requestModel)
        {
            requestModel.UserId = Request.Headers["AppId"];
            var result = await _userInterestManager.UpdateUserInterestAsync(requestModel);
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
            var requestModel = new DeleteUserInterestCommandRequest
            {
                Id = id
            };

            var result = await _userInterestManager.DeleteUserInterestAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
