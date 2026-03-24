using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorServices.GetMedicalRecordService;
using System.Security.Claims;

namespace MS.API.Controllers.DoctorController
{
    [Route("api/v1/doctor/appointments")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class GetMedicalRecordController : ControllerBase
    {
        private readonly IGetMedicalRecordService _service;

        public GetMedicalRecordController(IGetMedicalRecordService service)
        {
            _service = service;
        }

        [HttpGet("{appointmentId}/medical-record")]
        public async Task<IActionResult> Execute(Guid appointmentId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _service.Process(userId, appointmentId);

            return Ok(result);
        }
    }
}