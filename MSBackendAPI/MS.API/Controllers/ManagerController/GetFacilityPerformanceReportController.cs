using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// Controller for manager to retrieve facility performance reports.
    /// </summary>
    [Authorize(Roles = "Manager")]
    [Route("api/v1/manager/reports")]
    [ApiController]
    public class GetFacilityPerformanceReportController : ControllerBase
    {
        private readonly IGetFacilityPerformanceReportService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityPerformanceReportController"/> class.
        /// </summary>
        /// <param name="service">Service for facility performance report.</param>
        public GetFacilityPerformanceReportController(IGetFacilityPerformanceReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves performance report for a facility within an optional date range and optional doctor filter.
        /// </summary>
        /// <param name="request">Query parameters containing facility id, date range, and optional doctor id.</param>
        /// <returns>Performance report data.</returns>
        [HttpGet("performance")]
        public async Task<IActionResult> GetPerformanceReport([FromQuery] GetFacilityPerformanceReportRequest request)
        {
            var result = await _service.Process(request);
            return Ok(result);
        }
    }
}
