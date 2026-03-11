using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.CreateFacilityService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations
    /// related to creating new facilities.
    /// </summary>
    [Route("api/v1/admin/facilities")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminCreateFacilityController : ControllerBase
    {
        private readonly ICreateFacilityService _service;

        /// <summary>
        /// Initializes a new instance of the controller.
        /// </summary>
        public AdminCreateFacilityController(ICreateFacilityService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new facility in the system.
        /// </summary>
        /// <param name="request">
        /// Request containing facility information.
        /// </param>
        /// <returns>
        /// Returns the ID of the newly created facility.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateFacilityRequest request)
            => Ok(await _service.Process(request));
    }
}