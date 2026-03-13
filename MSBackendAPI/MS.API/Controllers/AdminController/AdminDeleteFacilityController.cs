using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.DeleteFacilityService;

namespace MS.API.Controllers.AdminControllers
{
    /// <summary>
    /// Defines API endpoints for administrative facility management operations.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to perform operations such as soft deleting facilities.
    /// All routes are prefixed with 'api/v1/admin/facilities' and require the ITAdmin role.
    /// </remarks>
    [ApiController]
    [Route("api/v1/admin/facilities")]
    [Authorize(Roles = "ITAdmin")]
    public class AdminDeleteFacilityController : ControllerBase
    {
        private readonly IDeleteFacilityService _deleteFacilityService;

        /// <summary>
        /// Initializes a new instance of the FacilityController class.
        /// </summary>
        /// <param name="deleteFacilityService">
        /// Service responsible for facility deletion operations.
        /// </param>
        public AdminDeleteFacilityController(IDeleteFacilityService deleteFacilityService)
        {
            _deleteFacilityService = deleteFacilityService;
        }

        /// <summary>
        /// Soft delete facility by identifier
        /// </summary>
        /// <param name="id">The unique identifier of the facility.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the delete operation.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(Guid id)
        {
            var result = await _deleteFacilityService.Process(id);
            return Ok(result);
        }
    }
}