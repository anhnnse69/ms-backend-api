using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.GetUserProfileService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller for retrieving the authenticated user's personal profile.
    /// </summary>
    [Authorize]
    [Route("api/v1/users")]
    [ApiController]
    public class GetUserProfileController : ControllerBase
    {
        private readonly IGetUserProfileService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserProfileController"/> class.
        /// </summary>
        /// <param name="service">Service for retrieving user profile data.</param>
        public GetUserProfileController(IGetUserProfileService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the personal profile of the currently authenticated user.
        /// </summary>
        /// <returns>
        /// Returns the user's profile including display name, full name, email, phone number, and avatar URL.
        /// </returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdClaim!.Value);
            var result = await _service.Process(userId);
            return Ok(result);
        }
    }
}
