using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.GetDoctorAppointmentService;
using MS.Domain.Entities;

namespace MS.API.Controllers.Doctor
{
    /// <summary>
    /// API controller responsible for retrieving doctor appointments
    /// </summary>
    [Route("api/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class GetDoctorAppointmentsController : ControllerBase
    {
        private readonly IGetDoctorAppointmentService _service;

        /// <summary>
        /// Constructor for GetDoctorAppointmentsController
        /// </summary>
        /// <param name="service">Service used to retrieve doctor appointment data</param>
        public GetDoctorAppointmentsController(IGetDoctorAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to retrieve appointment list of the current doctor
        /// </summary>
        /// <returns>Returns doctor appointment list</returns>
        [HttpGet]
        public async Task<IActionResult> Execute()
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
            var result = await _service.Process(userId);
            // 5. Return API response
            return Ok(result);
        }
    }
}