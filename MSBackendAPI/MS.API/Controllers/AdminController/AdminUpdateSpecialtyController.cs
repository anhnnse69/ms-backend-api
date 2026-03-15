using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.UpdateSpecialtyService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Handles updating specialty information.
    /// </summary>
    [Route("api/v1/admin/specialties")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminUpdateSpecialtyController : ControllerBase
    {
        private readonly IUpdateSpecialtyService _updateSpecialtyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminUpdateSpecialtyController"/> class.
        /// </summary>
        /// <param name="updateSpecialtyService">
        /// The service responsible for handling specialty update operations.
        /// </param>
        public AdminUpdateSpecialtyController(IUpdateSpecialtyService updateSpecialtyService)
        {
            _updateSpecialtyService = updateSpecialtyService;
        }

        /// <summary>
        /// Updates specialty information.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the specialty.
        /// </param>
        /// <param name="request">
        /// The request containing updated specialty data.
        /// </param>
        /// <returns>
        /// Returns the update result.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialty(Guid id, [FromBody] UpdateSpecialtyRequest request)
        {
            var result = await _updateSpecialtyService.Process(id, request);
            return Ok(result);
        }
    }
}