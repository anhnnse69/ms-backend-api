using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.GetAllDoctorsService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to doctor management.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints that allow administrators to retrieve doctor data.
    /// All routes are prefixed with 'api/v1/admin/doctors' and require the ITAdmin role for authorization.
    /// </remarks>
    [Route("api/v1/admin/doctors")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminGetAllDoctorsController : ControllerBase
    {
        private readonly IGetAllDoctorsService _service;

        /// <summary>
        /// Initializes a new instance of the AdminGetAllDoctorsController class with the specified doctor service.
        /// </summary>
        /// <param name="service">
        /// The service responsible for retrieving doctor data from the system.
        /// </param>
        public AdminGetAllDoctorsController(IGetAllDoctorsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to retrieve all doctors with pagination support.
        /// </summary>
        /// <param name="page">Current page index (default = 1)</param>
        /// <param name="size">Number of records per page (default = 10)</param>
        /// <returns>
        /// Returns a paginated list of doctors including metadata information.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _service.Process(page, size);
            return Ok(result);
        }
    }
}