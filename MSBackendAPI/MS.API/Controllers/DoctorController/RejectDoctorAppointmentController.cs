using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.RejectDoctorAppointmentService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for rejecting doctor appointment
    /// </summary>
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class RejectDoctorAppointmentController : ControllerBase
    {
        private readonly IRejectDoctorAppointmentService _service;

        /// <summary>
        /// Constructor for RejectDoctorAppointmentController
        /// </summary>
        /// <param name="service">Service used to reject appointment</param>
        public RejectDoctorAppointmentController(IRejectDoctorAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to reject doctor appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Reject appointment request</param>
        /// <returns>Rejected appointment response</returns>
        [HttpPost("{appointmentId}/reject")]
        public async Task<IActionResult> Execute(
            Guid appointmentId,
            [FromBody] RejectDoctorAppointmentRequest request)
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
            // 4. Assign appointmentId from route to request
            request.AppointmentId = appointmentId;
            // 5. Call service to process request
            var result = await _service.Process(userId, request);
            // 6. Return API response
            return Ok(result);
        }
    }
}