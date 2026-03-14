using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.AppointmentByFacilityService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// Controller responsible for managing appointment retrieval
    /// for a specific facility by manager role.
    /// </summary>
    [ApiController]
    [Route("api/v1/manager/appointments")]
    [Authorize(Roles = "Manager")]
    public class AppointmentByFacilityController : ControllerBase
    {
        private readonly IGetAppointmentByFacilityService _service;

        /// <summary>
        /// Initializes a new instance of the controller.
        /// </summary>
        /// <param name="service">
        /// Service used to retrieve appointments filtered by facility and other criteria.
        /// </param>
        public AppointmentByFacilityController(IGetAppointmentByFacilityService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of appointments for a facility.
        /// </summary>
        /// <param name="request">
        /// Appointment filtering request containing facility identifier,
        /// optional doctor filter, status filter, date filter,
        /// and pagination parameters.
        /// </param>
        /// <returns>
        /// API response containing the list of appointments with pagination metadata.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAppointments(
            [FromQuery] AppointmentFilterRequest request)
        {
            var result = await _service.Process(request);
            return Ok(result);
        }
    }
}