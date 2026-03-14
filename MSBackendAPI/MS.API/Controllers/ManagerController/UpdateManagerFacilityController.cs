using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.ManagerServices.UpdateFacilityService;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// Controller responsible for managing facility information by Manager role.
    /// Provides API endpoints for updating facility details.
    /// </summary>
    [ApiController]
    [Route("api/v1/manager/facility")]
    [Authorize(Roles = "Manager")]
    public class UpdateManagerFacilityController : ControllerBase
    {
        private readonly IUpdateManagerFacilityService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateManagerFacilityController"/> class.
        /// </summary>
        /// <param name="service">
        /// Service responsible for processing facility update operations.
        /// </param>
        public UpdateManagerFacilityController(IUpdateManagerFacilityService service)
        {
            _service = service;
        }

        /// <summary>
        /// Updates facility information managed by the manager.
        /// </summary>
        /// <param name="request">
        /// Request object containing facility identifier and updated facility information.
        /// </param>
        /// <returns>
        /// Returns API response indicating whether the facility update operation was successful.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFacility(
            [FromBody] UpdateManagerFacilityRequest request)
        {
            var result = await _service.Process(request);
            return Ok(result);
        }
    }
}
