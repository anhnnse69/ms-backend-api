using Microsoft.AspNetCore.Authorization;
using MS.Domain.Enums.GeneralCodes;
using Microsoft.AspNetCore.Mvc;
using MS.API.Helpers;
using MS.Domain.Shared.Utility;
using MS.Application.Services.CommonServices.LoginService;

namespace MS.API.Controllers.CommonController
{

    /// <summary>
    /// Defines API endpoints for user authentication operations such as login.
    /// </summary>
    /// <remarks>This controller provides endpoints related to authentication and is intended to be used as
    /// part of the application's API layer. All routes are prefixed with 'api/v1/auth'.</remarks>
    [Route("api/v1/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        /// <summary>
        /// Initializes a new instance of the LoginController class with the specified logger and login service.
        /// </summary>
        /// <param name="logger">The logger used to record authentication-related events and errors.</param>
        /// <param name="loginService">The service responsible for handling user login operations.</param>
        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        /// <summary>
        /// Authenticates a user with the provided login credentials and returns the result of the login attempt.
        /// </summary>
        /// <param name="loginRequest">The login credentials and related information submitted by the client. Must not be null and must satisfy all
        /// validation requirements.</param>
        /// <returns>An <see cref="IActionResult"/> containing the result of the login operation. Returns a 200 OK response with
        /// the authentication result if successful; otherwise, returns a 400 Bad Request if the input is invalid.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                var logModleStateMessage = ModelStateHelper.FormatErrors(LoggerMessageCode.APP_MESSAGE_3000.ToString(), ModelState);
                _logger.LogWarning($"{logModleStateMessage}");
                return BadRequest(ModelState);
            }
            var response = await _loginService.Proccess(loginRequest);
            if (MessageCode.APP_MESSAGE_4016.ToString().Equals(response.CodeMessage))
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
