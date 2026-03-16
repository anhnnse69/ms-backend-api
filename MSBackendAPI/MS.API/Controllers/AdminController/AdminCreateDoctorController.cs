using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.CreateDoctorService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations
    /// related to creating new doctors.
    /// </summary>
    [Route("api/v1/admin/doctors")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminCreateDoctorController : ControllerBase
    {
        private readonly ICreateDoctorService _service;

        /// <summary>
        /// Initializes a new instance of the controller.
        /// </summary>
        /// <param name="service">
        /// Service responsible for handling doctor creation logic.
        /// </param>
        public AdminCreateDoctorController(ICreateDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="request">
        /// Request containing doctor profile information.
        /// </param>
        /// <returns>
        /// Returns the ID of the newly created doctor.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorRequest request)
            => Ok(await _service.Process(request));
    }
}