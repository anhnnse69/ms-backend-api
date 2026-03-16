using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.CancelAppointmentService;

namespace MS.API.Controllers.PatientController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CancelAppointmentController : ControllerBase
    {
        private readonly ICancelAppointmentService _cancelAppointmentService;

        /// <summary>
        /// Initializes a new instance of the GetPatientAppointmentController
        /// </summary>
        /// <param name="cancelAppointmentService">The service for appointment cancellation logic</param>
        public CancelAppointmentController(ICancelAppointmentService cancelAppointmentService)
        {
            _cancelAppointmentService = cancelAppointmentService;
        }

        /// <summary>
        /// Endpoint to cancel a scheduled appointment
        /// </summary>
        /// <param name="request">The cancellation request containing ID and reason</param>
        /// <returns>An ApiResponse with the result of the operation</returns>
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentRequest request)
        {
            var result = await _cancelAppointmentService.Proccess(request);
            return Ok(result);
        }
    }
}
