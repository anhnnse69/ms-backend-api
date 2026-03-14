using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.PendingAppointmentPatientService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// API controller for retrieving pending patient appointment bookings
    /// </summary>
    [ApiController]
    [Route("api/v1/manager/appointments")]
    [Authorize(Roles = "Manager")]
    public class PendingAppointmentPatientController : ControllerBase
    {
        private readonly IGetPendingAppointmentPatientService _service;

        /// <summary>
        /// Initializes a new instance of the controller
        /// </summary>
        /// <param name="service">Service for retrieving pending appointment patients</param>
        public PendingAppointmentPatientController(IGetPendingAppointmentPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieve a paginated list of patients who have pending appointment bookings
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of pending appointment patients</returns>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingAppointments(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _service.Process(page, size);
            return Ok(result);
        }
    }
}