using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.API.Helpers;
using MS.Application.Services.CommonServices.RegisterService;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Defines API endpoints for user registration operations.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints related to patient account registration
    /// and is intended to be used as part of the application's API layer.
    /// All routes are prefixed with 'api/v1/auth'.
    /// </remarks>
    [Route("api/v1/auth")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IRegisterService _registerService;

        /// <summary>
        /// Initializes a new instance of the RegisterController class
        /// with the specified logger and register service.
        /// </summary>
        /// <param name="logger">The logger used to record registration-related events and errors.</param>
        /// <param name="registerService">The service responsible for handling patient registration operations.</param>
        public RegisterController(ILogger<RegisterController> logger, IRegisterService registerService)
        {
            _logger = logger;
            _registerService = registerService;
        }

        /// <summary>
        /// Registers a new patient account with the provided registration information.
        /// </summary>
        /// <param name="registerRequest">
        /// The registration information submitted by the client.
        /// Must not be null and must satisfy all validation requirements.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the registration operation.
        /// Returns a 200 OK response if successful;
        /// otherwise, returns a 400 Bad Request if the input is invalid or email already exists.
        /// </returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                var logModelStateMessage = ModelStateHelper.FormatErrors(LoggerMessageCode.APP_MESSAGE_3000.ToString(), ModelState);
                _logger.LogWarning($"{logModelStateMessage}");
                return BadRequest(ModelState);
            }
            var response = await _registerService.Process(registerRequest);
            if (MessageCode.APP_MESSAGE_4017.ToString().Equals(response.CodeMessage))
            {
                var logWarningMessage = LoggerMessageHelper.MessageAPIFormater(LoggerMessageCode.APP_MESSAGE_3001.ToString(), response.CodeMessage);
                _logger.LogWarning($"{logWarningMessage}");
                return BadRequest(response);
            }
            var logInfoMessage = LoggerMessageHelper.MessageAPIFormater(MessageCode.APP_MESSAGE_2000.ToString(), response.CodeMessage);
            _logger.LogInformation($"{logInfoMessage}");
            return Ok(response);
        }
    }
}