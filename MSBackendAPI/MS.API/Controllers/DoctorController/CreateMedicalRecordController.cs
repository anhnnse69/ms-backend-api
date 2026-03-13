using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.CreateMedicalRecordService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for creating medical record for a doctor appointment
    /// </summary>
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class CreateMedicalRecordController : ControllerBase
    {
        private readonly ICreateMedicalRecordService _service;

        /// <summary>
        /// Constructor for CreateMedicalRecordController
        /// </summary>
        /// <param name="service">Service responsible for creating medical record</param>
        public CreateMedicalRecordController(ICreateMedicalRecordService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to create medical record for a specific appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Create medical record request model</param>
        /// <returns>API response containing created medical record information</returns>
        [HttpPost("{appointmentId}/medical-record")]
        public async Task<IActionResult> Execute(
            Guid appointmentId,
            [FromBody] CreateMedicalRecordRequest request)
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