using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.BookAppointmentService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller responsible for handling patient appointment booking requests.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/appointments")]
    [Authorize(Roles = "Patient")]
    public class BookPatientAppointmentController : ControllerBase
    {
        private readonly IBookAppointmentService _bookAppointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookPatientAppointmentController"/> class.
        /// </summary>
        /// <param name="bookAppointmentService">Service responsible for appointment booking logic.</param>
        public BookPatientAppointmentController(IBookAppointmentService bookAppointmentService)
        {
            _bookAppointmentService = bookAppointmentService;
        }

        /// <summary>
        /// Books a new medical appointment for the authenticated patient.
        /// </summary>
        /// <param name="request">The request containing appointment booking details.</param>
        /// <returns>
        /// Returns the created appointment identifier and status upon success.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdClaim!.Value);
            var result = await _bookAppointmentService.Process(request, userId);
            return Ok(result);
        }
    }
}
