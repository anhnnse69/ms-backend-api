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

        public UpdateDoctorAppointmentStatusController(
            IUpdateDoctorAppointmentStatusService service)
        {
            _service = service;
        }

        [HttpPost("{appointmentId}/status")]
        public async Task<IActionResult> Execute(
            Guid appointmentId,
            [FromBody] UpdateDoctorAppointmentStatusRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            request.AppointmentId = appointmentId;

            var result = await _service.Process(userId, request);

            return Ok(result);
        }
    }
}
