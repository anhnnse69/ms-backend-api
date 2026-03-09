using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.GetAllFacilitiesService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to facility management.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to retrieve a paginated list of facilities in the system.
    /// All routes are prefixed with 'api/v1/admin/facilities' and require the ITAdmin role for authorization.
    /// </remarks>
    [Route("api/v1/admin/facilities")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminGetAllFacilitiesController : ControllerBase
    {
        private readonly IGetAllFacilitiesService _service;

        /// <summary>
        /// Initializes a new instance of the AdminGetAllFacilitiesController class.
        /// </summary>
        /// <param name="service">
        /// The service responsible for retrieving facility information.
        /// </param>
        public AdminGetAllFacilitiesController(IGetAllFacilitiesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of facilities.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the facility list with pagination metadata.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _service.Process(page, size);
            return Ok(result);
        }
    }
}