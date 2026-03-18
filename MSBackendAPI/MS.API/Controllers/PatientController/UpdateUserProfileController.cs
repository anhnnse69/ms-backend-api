using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.UpdateUserProfileService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller for updating the authenticated user's personal profile.
    /// </summary>
    [Authorize]
    [Route("api/v1/users")]
    [ApiController]
    public class UpdateUserProfileController : ControllerBase
    {
        private readonly IUpdateUserProfileService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserProfileController"/> class.
        /// </summary>
        /// <param name="service">Service for updating user profile data.</param>
        public UpdateUserProfileController(IUpdateUserProfileService service)
        {
            _service = service;
        }

        /// <summary>
        /// Updates the personal profile of the currently authenticated user.
        /// </summary>
        /// <param name="request">The request body containing the updated display name, full name, phone number, and avatar URL.</param>
        /// <returns>
        /// Returns true wrapped in an <see cref="ApiResponse{T}"/> if the update was successful.
        /// </returns>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdClaim!.Value);
            var result = await _service.Process(request, userId);
            return Ok(result);
        }
    }
}
