using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.DoctorDetailByFacilityService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// API controller for retrieving doctor detail information.
    /// </summary>
    /// <remarks>
    /// Provides endpoints that allow managers to view detailed information
    /// of a specific doctor.
    /// Routes are prefixed with 'api/doctors' and require the Manager role.
    /// </remarks>
    [ApiController]
    [Route("api/v1/manager/doctors")]
    [Authorize(Roles = "Manager")]
    public class DoctorDetailByFacilityController : ControllerBase
    {
        private readonly IDoctorDetailByFacilityService _service;
        /// <summary>
        /// Initializes the controller with the doctor detail service.
        /// </summary>
        /// <param name="service">Service used to retrieve doctor detail.</param>
        public DoctorDetailByFacilityController(IDoctorDetailByFacilityService service)
        {
            _service = service;
        }
        /// <summary>
        /// Retrieves detailed information of a specific doctor.
        /// </summary>
        /// <param name="doctorId">Doctor identifier.</param>
        /// <returns>Doctor detail information.</returns>
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDetail(Guid doctorId)
        {
            var result = await _service.Process(doctorId);
            return Ok(result);
        }
    }
}