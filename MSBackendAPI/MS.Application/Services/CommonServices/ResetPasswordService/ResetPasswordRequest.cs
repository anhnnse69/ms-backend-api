using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Represents the request payload for resetting a user's password via email token.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>The email address of the user requesting the password reset.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; }

        /// <summary>The password reset token received via email.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Token { get; set; }

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
