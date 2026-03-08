using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.CreateUser;

namespace MS.API.Controllers.Admin.ManagementUser
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to creating new users.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to create new user accounts in the system.
    /// All routes are prefixed with 'api/v1/admin/users' and require the ITAdmin role.
    /// </remarks>
    [Route("api/v1/admin/users")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminCreateUserController : ControllerBase
    {
        private readonly ICreateUserService _service;

        /// <summary>
        /// Initializes a new instance of the AdminCreateUserController class.
        /// </summary>
        /// <param name="service">
        /// The service responsible for handling the user creation process.
        /// </param>
        public AdminCreateUserController(ICreateUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="request">
        /// The request object containing the information required to create a new user.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the create-user operation.
        /// Returns a 200 OK response with the newly created user's ID if successful.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
            => Ok(await _service.Process(request));
    }
}