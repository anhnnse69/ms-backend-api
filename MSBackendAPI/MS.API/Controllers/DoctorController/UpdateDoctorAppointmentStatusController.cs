using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for updating doctor appointment status
    /// </summary>
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class UpdateDoctorAppointmentStatusController : ControllerBase
    {
        private readonly IUpdateDoctorAppointmentStatusService _service;

        /// <summary>
        /// Constructor for UpdateDoctorAppointmentStatusController
        /// </summary>
        /// <param name="service">Service used to update appointment status</param>
        public UpdateDoctorAppointmentStatusController(IUpdateDoctorAppointmentStatusService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to update doctor appointment status
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Update appointment status request</param>
        /// <returns>Updated appointment response</returns>
        [HttpPost("{appointmentId}/status")]
        public async Task<IActionResult> Execute(Guid appointmentId, [FromBody] UpdateDoctorAppointmentStatusRequest request)
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