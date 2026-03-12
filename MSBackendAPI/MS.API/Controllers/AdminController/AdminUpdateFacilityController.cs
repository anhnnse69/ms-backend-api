using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.UpdateFacilityService;

namespace MS.API.Controllers.Admin.FacilityControllers
{
    /// <summary>
    /// Handles updating facility information.
    /// </summary>
    [Route("api/v1/admin/facilities")] 
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminUpdateFacilityController : ControllerBase
    {
        private readonly IUpdateFacilityService _updateFacilityService;

        /// <summary>
        /// Initializes a new instance of the UpdateFacilityController class.
        /// </summary>
        public AdminUpdateFacilityController(IUpdateFacilityService updateFacilityService)
        {
            _updateFacilityService = updateFacilityService;
        }

        /// <summary>
        /// Updates facility information.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the facility.
        /// </param>
        /// <param name="request">
        /// The request containing updated facility data.
        /// </param>
        /// <returns>
        /// Returns the update result.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacility( Guid id, [FromBody] UpdateFacilityRequest request)
        {
            var result = await _updateFacilityService.Process(id, request);
            return Ok(result);
        }
    }
}