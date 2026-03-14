using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.AppointmentReportService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// Controller responsible for managing appointment reports for managers.
    /// </summary>
    [ApiController]
    [Route("api/v1/manager/appointments")]
    [Authorize(Roles = "Manager")]
    public class AppointmentReportController : ControllerBase
    {
        private readonly IGetAppointmentReportService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentReportController"/> class.
        /// </summary>
        /// <param name="service">
        /// Service used to generate appointment report statistics.
        /// </param>
        public AppointmentReportController(IGetAppointmentReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves appointment report statistics for a specific facility.
        /// </summary>
        /// <param name="request">
        /// Query parameters containing facility identifier, report type
        /// (day, month, or year), and optional date filter.
        /// </param>
        /// <returns>
        /// Returns appointment statistics including counts of different
        /// appointment statuses such as pending, confirmed, completed, etc.
        /// </returns>
        [HttpGet("report")]
        public async Task<IActionResult> GetReport(
            [FromQuery] AppointmentReportRequest request)
        {
            var result = await _service.Process(request);
            return Ok(result);
        }
    }
}