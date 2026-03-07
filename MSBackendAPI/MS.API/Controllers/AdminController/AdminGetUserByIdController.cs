using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.GetUserByIdService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for retrieving user information by identifier for administrative operations.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to retrieve detailed user information using a unique user identifier.
    /// All routes are prefixed with 'api/v1/admin/users' and require the ITAdmin role for authorization.
    /// </remarks>
    [Route("api/v1/admin/users")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminGetUserByIdController : ControllerBase
    {
        private readonly IGetUserByIdService _service;

        /// <summary>
        /// Initializes a new instance of the AdminGetUserByIdController class with the specified user service.
        /// </summary>
        /// <param name="service">
        /// The service responsible for retrieving user data by identifier.
        /// </param>
        public AdminGetUserByIdController(IGetUserByIdService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves user information by the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the get-user-by-id operation.
        /// Returns a 200 OK response with the user information if the request is successful.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _service.Process(id));
    }
}