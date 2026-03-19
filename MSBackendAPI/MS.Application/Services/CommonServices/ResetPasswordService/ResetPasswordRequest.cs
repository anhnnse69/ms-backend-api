using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Represents the request payload for resetting a user's password via a scoped reset JWT.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>The short-lived scoped reset JWT issued by the verify-otp endpoint.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string ResetToken { get; set; }

        /// <summary>The new password to be set for the user account.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PasswordStrength(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NewPassword { get; set; }

        /// <summary>Confirmation of the new password; must match NewPassword.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [Compare(nameof(NewPassword), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string ConfirmPassword { get; set; }
    }
}
