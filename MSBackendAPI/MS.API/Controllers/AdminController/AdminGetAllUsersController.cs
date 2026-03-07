using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.GetAllUsersService;

namespace MS.API.Controllers.AdminController
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
        /// Execute API to retrieve all users with pagination support.
        /// </summary>
        /// <param name="page">Current page index (default = 1)</param>
        /// <param name="size">Number of records per page (default = 10)</param>
        /// <returns>
        /// Returns a paginated list of users including metadata information.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _service.Process(page, size);
            return Ok(result);
        }
    }
}