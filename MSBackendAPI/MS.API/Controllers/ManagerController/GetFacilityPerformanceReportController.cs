using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService;
using System.Security.Claims;

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
        /// Retrieves the performance report for the manager's facility within an optional date range and optional doctor filter.
        /// </summary>
        /// <param name="request">Query parameters containing optional facility id, date range, and optional doctor id.</param>
        /// <returns>
        /// Returns the facility performance report including overall metrics and per-doctor breakdown.
        /// </returns>
        [HttpGet("performance")]
        public async Task<IActionResult> GetPerformanceReport([FromQuery] GetFacilityPerformanceReportRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var managerId = Guid.Parse(userIdClaim!.Value);
            var result = await _service.Process(request, managerId);
            return Ok(result);
        }
    }
}