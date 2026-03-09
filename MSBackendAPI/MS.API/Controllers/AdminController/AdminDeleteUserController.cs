using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.DeleteUserService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to deleting users.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to delete existing users.
    /// All routes are prefixed with 'api/v1/admin/users' and require the ITAdmin role.
    /// </remarks>
    [Route("api/v1/admin/users")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminDeleteUserController : ControllerBase
    {
        private readonly IDeleteUserService _service;

        /// <summary>
        /// Initializes a new instance of the AdminDeleteUserController class.
        /// </summary>
        /// <param name="service">
        /// The service responsible for handling user delete operations.
        /// </param>
        public AdminDeleteUserController(IDeleteUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Deletes an existing user in the system (soft delete).
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to delete.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the delete operation.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Process(id);
            return Ok(result);
        }
    }
}