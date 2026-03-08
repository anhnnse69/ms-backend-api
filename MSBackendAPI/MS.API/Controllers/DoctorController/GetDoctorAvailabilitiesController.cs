using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.Doctors.GetDoctorAvailabilityService;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for retrieving doctor availability schedules
    /// </summary>
    [Route("api/v1/doctor/availabilities")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class GetDoctorAvailabilitiesController : ControllerBase
    {
        private readonly IGetDoctorAvailabilityService _service;

        /// <summary>
        /// Constructor for GetDoctorAvailabilitiesController
        /// </summary>
        /// <param name="service">Service used to retrieve doctor availability data</param>
        public GetDoctorAvailabilitiesController(IGetDoctorAvailabilityService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to retrieve the availability schedule of the current doctor
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Page size</param>
        /// <returns>Returns doctor availability list</returns>
        [HttpGet]
        public async Task<IActionResult> Execute(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            // 1. Retrieve userId from JWT token claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            // 2. Validate userId existence
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            // 3. Convert claim value to Guid
            var userId = Guid.Parse(userIdClaim.Value);
            // 4. Call service to process request
            var result = await _service.Process(userId, page, size);
            // 5. Return API response
            return Ok(result);
        }
    }
}