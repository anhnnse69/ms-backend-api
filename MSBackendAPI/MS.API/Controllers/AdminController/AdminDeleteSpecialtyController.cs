using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.DeleteSpecialtyService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Handles deleting specialty information.
    /// </summary>
    [Route("api/v1/admin/specialties")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminDeleteSpecialtyController : ControllerBase
    {
        private readonly IDeleteSpecialtyService _deleteSpecialtyService;

        /// <summary>
        /// Initializes a new instance of the AdminDeleteSpecialtyController class.
        /// </summary>
        /// <param name="deleteSpecialtyService">
        /// Service used to process specialty deletion.
        /// </param>
        public AdminDeleteSpecialtyController(IDeleteSpecialtyService deleteSpecialtyService)
        {
            _deleteSpecialtyService = deleteSpecialtyService;
        }

        /// <summary>
        /// Deletes a specialty (soft delete).
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the specialty.
        /// </param>
        /// <returns>
        /// Returns the delete result.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialty(Guid id)
        {
            var result = await _deleteSpecialtyService.Process(id);
            return Ok(result);
        }
    }
}