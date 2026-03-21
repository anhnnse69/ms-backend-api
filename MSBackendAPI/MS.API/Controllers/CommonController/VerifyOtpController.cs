using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.CommonServices.VerifyOtpService;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Handles OTP verification requests and issues a scoped reset JWT on success.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    [AllowAnonymous]
    public class VerifyOtpController : ControllerBase
    {
        private readonly IVerifyOtpService _verifyOtpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyOtpController"/> class.
        /// </summary>
        /// <param name="verifyOtpService">Service responsible for processing OTP verification requests.</param>
        public VerifyOtpController(IVerifyOtpService verifyOtpService)
        {
            _verifyOtpService = verifyOtpService;
        }

        /// <summary>
        /// Verifies the submitted OTP code and returns a short-lived scoped reset JWT on success.
        /// </summary>
        /// <param name="request">The request containing the OTP code to be verified.</param>
        /// <returns>
        /// Returns a scoped reset JWT if the OTP is valid; otherwise returns a failure response.
        /// </returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var result = await _verifyOtpService.Process(request);
            return Ok(result);
        }
    }
}
