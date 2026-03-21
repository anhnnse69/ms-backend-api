using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.VerifyOtpService
{
    /// <summary>
    /// Represents the request payload for verifying a password reset OTP.
    /// </summary>
    public class VerifyOtpRequest
    {
        /// <summary>The 6-digit OTP code received via email.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [StringLength(6, MinimumLength = 6, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string OtpCode { get; set; }
    }
}
