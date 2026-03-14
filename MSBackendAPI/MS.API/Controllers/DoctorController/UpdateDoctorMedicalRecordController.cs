using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for updating medical record for a doctor appointment
    /// </summary>
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class UpdateDoctorMedicalRecordController : ControllerBase
    {
        private readonly IUpdateDoctorMedicalRecordService _service;

        /// <summary>
        /// Constructor for UpdateDoctorMedicalRecordController
        /// </summary>
        /// <param name="service">Service responsible for updating medical record</param>
        public UpdateDoctorMedicalRecordController(
            IUpdateDoctorMedicalRecordService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to update medical record for a specific appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Update medical record request model</param>
        /// <returns>API response containing updated medical record information</returns>
        [HttpPut("{appointmentId}/medical-record")]
        public async Task<IActionResult> Execute(Guid appointmentId, [FromBody] UpdateDoctorMedicalRecordRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = Guid.Parse(userIdClaim.Value);
            var result = await _service.Process(userId, appointmentId, request);
            return Ok(result);
        }
    }
}
