using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.UpdateUserService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to updating users.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to update existing user information.
    /// All routes are prefixed with 'api/v1/admin/users' and require the ITAdmin role.
    /// </remarks>
    [Route("api/v1/admin/users")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminUpdateUserController : ControllerBase
    {
        private readonly IUpdateUserService _service;

        /// <summary>
        /// Initializes a new instance of the AdminUpdateUserController class.
        /// </summary>
        /// <param name="service">
        /// The service responsible for handling user update operations.
        /// </param>
        public AdminUpdateUserController(IUpdateUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to update.
        /// </param>
        /// <param name="request">
        /// The request object containing updated user information.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the update operation.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            var result = await _service.Process(id, request);
            return Ok(result);
        }
    }
}