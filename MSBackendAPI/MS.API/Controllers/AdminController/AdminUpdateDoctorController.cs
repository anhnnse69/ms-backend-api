using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.UpdateDoctorService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Controller responsible for handling admin operations related to updating doctors.
    /// </summary>
    [Route("api/v1/admin/doctors")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminUpdateDoctorController : ControllerBase
    {
        private readonly IUpdateDoctorService _service;

        /// <summary>
        /// Initializes a new instance of the AdminUpdateDoctorController class.
        /// </summary>
        /// <param name="service">
        /// Service used to process doctor update operations.
        /// </param>
        public AdminUpdateDoctorController(IUpdateDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Updates doctor information.
        /// </summary>
        /// <param name="request">
        /// The request object containing updated doctor data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the update operation.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDoctorRequest request)
            => Ok(await _service.Process(request));
    }
}