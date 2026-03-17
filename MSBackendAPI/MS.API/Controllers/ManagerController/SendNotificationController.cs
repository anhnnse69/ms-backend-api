using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.API.Helpers;
using MS.Application.Common.Response;
using MS.Application.Services.ManagerServices.SendNotificationService;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using System.Security.Claims;

namespace MS.API.Controllers.ManagerController
{
    /// <summary>
    /// Defines API endpoints for sending notifications to patients about their appointments.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints related to sending notifications to patients
    /// and is intended to be used as part of the application's API layer.
    /// All routes are prefixed with 'api/v1/manager/notifications'.
    /// Requires Manager role authorization.
    /// </remarks>
    [Route("api/v1/manager/notifications")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SendNotificationController : ControllerBase
    {
        private readonly ILogger<SendNotificationController> _logger;
        private readonly ISendNotificationService _sendNotificationService;

        /// <summary>
        /// Initializes a new instance of the SendNotificationController class
        /// with the specified logger and notification service.
        /// </summary>
        /// <param name="logger">The logger used to record notification-related events and errors.</param>
        /// <param name="sendNotificationService">The service responsible for handling notification operations.</param>
        public SendNotificationController(ILogger<SendNotificationController> logger, ISendNotificationService sendNotificationService)
        {
            _logger = logger;
            _sendNotificationService = sendNotificationService;
        }

        /// <summary>
        /// Sends a notification to a patient about their appointment via Manager request.
        /// </summary>
        /// <param name="sendNotificationRequest">
        /// The notification request containing patient ID, appointment details, and message content.
        /// Must not be null and must satisfy all validation requirements.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the notification operation.
        /// Returns a 200 OK response if successful;
        /// otherwise, returns a 400 Bad Request if the input is invalid, or 401 Unauthorized if token is invalid.
        /// </returns>
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest sendNotificationRequest)
        {
            if (!ModelState.IsValid)
            {
                var logModelStateMessage = ModelStateHelper.FormatErrors(LoggerMessageCode.APP_MESSAGE_3000.ToString(), ModelState);
                _logger.LogWarning($"{logModelStateMessage}");
                return BadRequest(ModelState);
            }
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out Guid managerId))
            {
                var logUnauthorizedMessage = LoggerMessageHelper.MessageAPIFormater(LoggerMessageCode.APP_MESSAGE_3001.ToString(), MessageCode.APP_MESSAGE_4020.ToString());
                _logger.LogWarning($"{logUnauthorizedMessage}");
                return Unauthorized(ApiResponse<SendNotificationResponse>.Fail(MessageCode.APP_MESSAGE_4020.ToString()));
            }
            var response = await _sendNotificationService.Process(sendNotificationRequest);
            if (MessageCode.APP_MESSAGE_4012.ToString().Equals(response.CodeMessage) ||
                MessageCode.APP_MESSAGE_4010.ToString().Equals(response.CodeMessage))
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