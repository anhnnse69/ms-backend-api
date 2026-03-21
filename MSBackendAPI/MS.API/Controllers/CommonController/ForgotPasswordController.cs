using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.CommonServices.ForgotPasswordService;
using Microsoft.AspNetCore.Authorization;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Handles forgot password requests for public (unauthenticated) users.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    [AllowAnonymous]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPasswordService _forgotPasswordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordController"/> class.
        /// </summary>
        /// <param name="forgotPasswordService">Service responsible for forgot password business logic.</param>
        public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        /// <summary>
        /// Initiates a password reset by sending a one-time verification code (OTP) to the provided email address.
        /// Always returns HTTP 200 regardless of whether the email exists to prevent enumeration.
        /// </summary>
        /// <param name="request">The request containing the user's email address.</param>
        /// <returns>
        /// Returns a generic success message confirming that the OTP-based reset request was processed.
        /// </returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _forgotPasswordService.Process(request);
            return Ok(result);
        }
    }
}
