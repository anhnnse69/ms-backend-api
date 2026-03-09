using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorsByFacilityService;

namespace MS.API.Controllers.DoctorsByFacility
{
    /// <summary>
    /// API controller for retrieving doctors by facility.
    /// </summary>
    /// <remarks>
    /// Provides endpoints that allow managers to view a paginated list of doctors
    /// working at a specific facility.
    /// Routes are prefixed with 'api/doctors' and require the Manager role.
    /// </remarks>
    [ApiController]
    [Route("api/manager")]
    [Authorize(Roles = "Manager")]
    public class DoctorByFacilityController : ControllerBase
    {
        private readonly IDoctorByFacilityService _service;
        /// <summary>
        /// Initializes the controller with the doctor-by-facility service.
        /// </summary>
        /// <param name="service">Service used to retrieve doctors by facility.</param>
        public DoctorByFacilityController(IDoctorByFacilityService service)
        {
            _service = service;
        }
        /// <summary>
        /// Retrieves doctors that belong to a specific facility.
        /// </summary>
        /// <param name="facilityId">The facility identifier.</param>
        /// <param name="page">Page index (default = 1).</param>
        /// <param name="size">Number of records per page (default = 10).</param>
        /// <returns>A paginated list of doctors for the given facility.</returns>
        [HttpGet("facility/{facilityId}")]
        public async Task<IActionResult> GetByFacility(
            Guid facilityId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _service.Process(facilityId, page, size);
            return Ok(result);
        }
    }
}