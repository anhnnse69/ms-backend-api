using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorAppointmentService;
using MS.Application.Services.DoctorAvailabilityService;
using MS.Domain.Entities;

namespace MS.API.Controllers.Doctor
{
    [Route("api/doctor")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _availabilityService;
        private readonly IDoctorAppointmentService _appointmentService;

        public DoctorScheduleController(
            IDoctorAvailabilityService availabilityService,
            IDoctorAppointmentService appointmentService)
        {
            _availabilityService = availabilityService;
            _appointmentService = appointmentService;
        }

        [HttpGet("availabilities")]
        public async Task<IActionResult> GetMyAvailabilities()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var doctorId = Guid.Parse(userIdClaim.Value);
            var result = await _availabilityService.GetMyAvailabilities(doctorId);
            return Ok(result);
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var doctorId = Guid.Parse(userIdClaim.Value);
            var result = await _appointmentService.GetMyAppointments(doctorId);
            return Ok(result);
        }
    }
}
