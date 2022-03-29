using Microsoft.AspNetCore.Mvc;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Manager.Abstraction;

namespace NewsApp.Api.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListUserQueryRequest requestModel)
        {
            var result = await _userManager.GetAllUserAsync(requestModel);
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
            var requestModel = new GetUserQueryRequest
            {
                Id = id
            };

            var result = await _userManager.GetUserAsync(requestModel);
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
        public async Task<IActionResult> Post([FromBody] CreateUserCommandRequest requestModel)
        {
            requestModel.AppId = Request.Headers["AppId"];

            var result = await _userManager.CreateUserAsync(requestModel);
            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommandRequest requestModel)
        {
            var result = await _userManager.UpdateUserAsync(requestModel);
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
            var requestModel = new DeleteUserCommandRequest
            {
                Id = id
            };

            var result = await _userManager.DeleteUserAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }

    }
}
