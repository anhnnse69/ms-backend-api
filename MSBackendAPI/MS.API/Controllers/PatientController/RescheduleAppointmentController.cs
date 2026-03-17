using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.UpdateAppointment;

namespace MS.API.Controllers.PatientController
{
    [ApiController]
    [Route("api/[controller]")]
    public class RescheduleAppointmentController : ControllerBase
    {
        private readonly IRescheduleAppointmentService _rescheduleAppointmentService;

        public RescheduleAppointmentController(IRescheduleAppointmentService rescheduleAppointmentService)
        {
            _rescheduleAppointmentService = rescheduleAppointmentService;
        }

        /// <summary>
        /// Reschedules an unconfirmed appointment using ID from request body
        /// </summary>
        /// <param name="request">Payload containing AppointmentId, NewDoctorId and NewAppointmentTime</param>
        [HttpPut("reschedule")]
        public async Task<IActionResult> RescheduleAppointment([FromBody] RescheduleAppointmentRequest request)
        {
            // Process directly using the request object
            var response = await _rescheduleAppointmentService.Process(request);
            return Ok(response);
        }
    }
}