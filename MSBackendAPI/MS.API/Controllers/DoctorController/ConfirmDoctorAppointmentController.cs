using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.ConfirmDoctorAppointmentService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for confirming doctor appointment
    /// </summary>
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class ConfirmDoctorAppointmentController : ControllerBase
    {
        private readonly IConfirmDoctorAppointmentService _service;

        /// <summary>
        /// Constructor for ConfirmDoctorAppointmentController
        /// </summary>
        /// <param name="service">Service used to confirm appointment</param>
        public ConfirmDoctorAppointmentController(IConfirmDoctorAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to confirm doctor appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Confirmed appointment response</returns>
        [HttpPost("{appointmentId}/confirm")]
        public async Task<IActionResult> Execute(Guid appointmentId)
        {
            // 1. Retrieve userId from JWT token claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            // 2. Validate userId existence
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            // 3. Convert claim value to Guid
            var userId = Guid.Parse(userIdClaim.Value);
            // 4. Call service to process request
            var result = await _service.Process(userId, appointmentId);
            // 5. Return API response
            return Ok(result);
        }
    }
}