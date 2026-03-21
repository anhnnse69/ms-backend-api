using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.DeleteFavoriteService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Handles requests for removing a favorite record from the patient's favorites list.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/favorites")]
    [Authorize(Roles = "Patient")]
    public class DeletePatientFavoriteController : ControllerBase
    {
        private readonly IDeleteFavoriteService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePatientFavoriteController"/> class.
        /// </summary>
        /// <param name="service">Service responsible for delete favorite business logic.</param>
        public DeletePatientFavoriteController(IDeleteFavoriteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Removes a favorite record from the authenticated patient's favorites list (soft delete).
        /// </summary>
        /// <param name="id">The unique identifier of the favorite record to remove.</param>
        /// <returns>
        /// Returns a success payload if the favorite was removed; otherwise returns a validation error.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }
            var result = await _service.Process(id, userId);
            return Ok(result);
        }
    }
}
