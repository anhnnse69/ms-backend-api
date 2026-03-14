using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.GetAllSpecialtiesService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// API endpoints for managing specialties.
    /// </summary>
    [Route("api/v1/admin/specialties")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminGetAllSpecialtiesController : ControllerBase
    {
        private readonly IGetAllSpecialtiesService _service;

        /// <summary>
        /// Initializes a new instance of AdminGetAllSpecialtiesController.
        /// </summary>
        public AdminGetAllSpecialtiesController(IGetAllSpecialtiesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieve all specialties with pagination.
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>List of specialties</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _service.Process(page, size);
            return Ok(result);
        }
    }
}