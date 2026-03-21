using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.AddFavoriteService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Handles requests for adding a doctor or facility to the patient's favorites.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/favorites")]
    [Authorize(Roles = "Patient")]
    public class AddPatientFavoriteController : ControllerBase
    {
        private readonly IAddFavoriteService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPatientFavoriteController"/> class.
        /// </summary>
        /// <param name="service">Service responsible for add favorite business logic.</param>
        public AddPatientFavoriteController(IAddFavoriteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a doctor or facility to the authenticated patient's favorites list.
        /// </summary>
        /// <param name="request">The request containing doctorId or facilityId to favorite.</param>
        /// <returns>
        /// Returns the created or restored favorite record on success.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }
            var result = await _service.Process(request, userId);
            return Ok(result);
        }
    }
}
