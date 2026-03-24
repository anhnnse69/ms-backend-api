using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PaymentServices.Momo;
using MS.Infrastructure.Common.Services.MomoPaymentService;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.API.Controllers.PaymentController
{
    /// <summary>
    /// Handles MoMo payment creation and callbacks.
    /// </summary>
    [ApiController]
    [Route("api/v1/payments/momo")]
    public class MomoPaymentsController : ControllerBase
    {
        private readonly ICreateMomoPaymentService _createMomoPaymentService;
        private readonly IMomoPaymentService _momoPaymentService;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;

        public MomoPaymentsController(
            ICreateMomoPaymentService createMomoPaymentService,
            IMomoPaymentService momoPaymentService,
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment)
        {
            _createMomoPaymentService = createMomoPaymentService;
            _momoPaymentService = momoPaymentService;
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
        }

        /// <summary>
        /// Creates a MoMo payment session for the specified appointment and returns the redirect URL.
        /// </summary>
        [HttpPost("create")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreatePayment([FromBody] CreateMomoPaymentRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value) || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }
            var apiResponse = await _createMomoPaymentService.Process(request, userId);
            return Ok(apiResponse);
        }

        /// <summary>
        /// Synchronous redirect callback from MoMo after user completes payment in the app.
        /// This endpoint mainly exists so the configured ReturnUrl is valid; the deposit
        /// confirmation logic relies on the IPN callback.
        /// </summary>
        [HttpGet("return")]
        [AllowAnonymous]
        public IActionResult Return()
        {
            // Redirect the user from the backend callback URL to the frontend UI
            // while preserving all MoMo query string parameters so the FE can
            // show a proper payment result screen.
            // NOTE: For now, the frontend base URL and locale are hard-coded for
            // local development. You can move these to configuration if needed.
            // Use HTTP here because the Next.js dev server typically runs on http://localhost:3000
            const string frontendBaseUrl = "http://localhost:3000";
            // Dedicated MoMo result page in the frontend
            const string frontendPath = "/vi/patient/payment/momo-return";
            var queryString = Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty;
            var redirectUrl = $"{frontendBaseUrl}{frontendPath}{queryString}";
            return Redirect(redirectUrl);
        }

        /// <summary>
        /// IPN callback from MoMo used to confirm whether the deposit has been paid.
        /// </summary>
        [HttpPost("ipn")]
        [AllowAnonymous]
        public async Task<IActionResult> Ipn([FromBody] MomoIpnRequest request)
        {
            if (!_momoPaymentService.ValidateIpnSignature(request))
            {
                return BadRequest(new { message = "Invalid signature" });
            }
            if (string.IsNullOrWhiteSpace(request.ExtraData))
            {
                return Ok(new { resultCode = 1, message = "Missing extraData" });
            }
            Guid appointmentId;
            try
            {
                var extraDataRaw = Encoding.UTF8.GetString(Convert.FromBase64String(request.ExtraData));
                // Expected format: "appointmentId={guid}"
                var parts = extraDataRaw.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2 || !Guid.TryParse(parts[1], out appointmentId))
                {
                    return Ok(new { resultCode = 1, message = "Invalid extraData format" });
                }
            }
            catch
            {
                return Ok(new { resultCode = 1, message = "Failed to decode extraData" });
            }
            var appointment = await _getAppointmentById.Execute(appointmentId);
            if (appointment == null)
            {
                return Ok(new { resultCode = 1, message = "Appointment not found" });
            }
            if (request.ResultCode == 0)
            {
                appointment.IsDepositPaid = true;
                appointment.PaymentMethod = "MoMo";
                appointment.PaymentTransactionId = request.OrderId;
                await _updateAppointment.Execute(appointment);
            }
            // MoMo expects HTTP 200 with a JSON body containing at least resultCode / message
            return Ok(new { resultCode = 0, message = "IPN processed" });
        }
    }
}
