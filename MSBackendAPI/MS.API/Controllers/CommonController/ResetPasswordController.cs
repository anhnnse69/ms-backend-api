using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.CommonServices.ResetPasswordService;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Handles password reset requests for public (unauthenticated) users.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    [AllowAnonymous]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IResetPasswordService _resetPasswordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordController"/> class.
        /// </summary>
        /// <param name="resetPasswordService">Service responsible for reset password business logic.</param>
        public ResetPasswordController(IResetPasswordService resetPasswordService)
        {
            _resetPasswordService = resetPasswordService;
        }

        /// <summary>
        /// Resets the user's password using a valid password reset token (for example, a scoped reset JWT).
        /// Validates the token and updates the user's password according to the reset policy.
        /// </summary>
        /// <param name="request">The request containing the reset token and the new password (and confirmation, if required).</param>
        /// <returns>
        /// Returns a success payload if the password was reset; otherwise returns a validation or error response.
        /// </returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _resetPasswordService.Process(request);
            return Ok(result);
        }
    }
}
