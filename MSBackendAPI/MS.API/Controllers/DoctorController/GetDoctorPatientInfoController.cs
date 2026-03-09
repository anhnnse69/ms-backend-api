using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.GetDoctorPatientInfoService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    /// <summary>
    /// API controller responsible for retrieving patient information for doctor
    /// </summary>
    [ApiController]
    [Route("api/v1/doctor/appointments")]
    [Authorize(Roles = "Doctor")]
    public class GetDoctorPatientInfoController : ControllerBase
    {
        private readonly IGetDoctorPatientInfoService _service;

        /// <summary>
        /// Constructor for GetDoctorPatientInfoController
        /// </summary>
        /// <param name="service">Service used to retrieve patient information</param>
        public GetDoctorPatientInfoController(
            IGetDoctorPatientInfoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to retrieve patient information associated with appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Patient information response</returns>
        [HttpGet("{appointmentId}/patient")]
        public async Task<IActionResult> GetPatientInfo(Guid appointmentId)
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