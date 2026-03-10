using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorScheduleService;

namespace MS.API.Controllers.DoctorSchedule
{
    /// <summary>
    /// API controller for retrieving doctor schedules by facility
    /// </summary>
    [ApiController]
    [Route("api/v1/manager/doctors")]
    [Authorize(Roles = "Manager")]
    public class DoctorScheduleByFacilityController : ControllerBase
    {
        private readonly IDoctorScheduleService _service;
        /// <summary>
        /// Initializes a new instance of the controller
        /// </summary>
        /// <param name="service">Doctor schedule service</param>
        public DoctorScheduleByFacilityController(IDoctorScheduleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieve doctor schedules by facility identifier
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of doctor schedules</returns>
        [HttpGet("schedule/{facilityId}")]
        public async Task<IActionResult> GetSchedules(
        Guid facilityId,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
        {
            var result = await _service.Process(facilityId, page, size);
            return Ok(result);
        }
    }
}