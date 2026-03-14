using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.CreateSpecialtyService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations
    /// related to creating new specialties.
    /// </summary>
    [Route("api/v1/admin/specialties")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminCreateSpecialtyController : ControllerBase
    {
        private readonly ICreateSpecialtyService _service;

        /// <summary>
        /// Initializes a new instance of the controller.
        /// </summary>
        /// <param name="service">
        /// Service responsible for handling specialty creation logic.
        /// </param>
        public AdminCreateSpecialtyController(ICreateSpecialtyService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new specialty in the system.
        /// </summary>
        /// <param name="request">
        /// Request containing specialty information.
        /// </param>
        /// <returns>
        /// Returns the ID of the newly created specialty.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecialtyRequest request)
            => Ok(await _service.Process(request));
    }
}