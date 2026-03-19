using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.CommonServices.ResetPasswordService;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Handles password reset requests for public (unauthenticated) users.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
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
        /// Resets the user's password using a valid email reset token.
        /// Validates the token, updates the password hash, and marks the token as used.
        /// </summary>
        /// <param name="request">The request containing email, token, new password, and confirmation.</param>
        /// <returns>
        /// Returns a success message if the password was reset; otherwise returns a validation error.
        /// </returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _resetPasswordService.Process(request);
            return Ok(result);
        }
    }
}
