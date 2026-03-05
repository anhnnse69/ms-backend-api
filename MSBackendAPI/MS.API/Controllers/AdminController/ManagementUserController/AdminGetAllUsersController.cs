using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.UserService.GetAllUsersService;

namespace MS.API.Controllers.Admin.ManagementUser
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to user management.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints that allow administrators to retrieve and manage user data.
    /// All routes are prefixed with 'api/v1/admin/users' and require the ITAdmin role for authorization.
    /// </remarks>
    [Route("api/v1/admin/users")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminGetAllUsersController : ControllerBase
    {
        private readonly IGetAllUsersService _service;

        /// <summary>
        /// Initializes a new instance of the AdminGetAllUsersController class with the specified user service.
        /// </summary>
        /// <param name="service">
        /// The service responsible for retrieving user data from the system.
        /// </param>
        public AdminGetAllUsersController(IGetAllUsersService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the get-all-users operation.
        /// Returns a 200 OK response with a list of users if the request is successful.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.Process());
    }
}