using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MS.Application.Services.PatientServices.ChangePasswordService
{
    /// <summary>
    /// Request model for change password.
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>User Id — populated from JWT token, not from request body.</summary>
        [JsonIgnore]
        public Guid UserId { get; set; }

        /// <summary>Current password of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 8, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>New password — must meet strength requirements.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PasswordStrength(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 8, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NewPassword { get; set; } = string.Empty;
    }
}