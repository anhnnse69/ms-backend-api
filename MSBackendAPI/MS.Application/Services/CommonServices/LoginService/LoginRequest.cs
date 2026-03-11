using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.LoginService
{
    /// <summary>
    /// Request object for user login.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>Email address used to identify the user account.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(150, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>Password for the user account.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 8, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Password { get; set; } = string.Empty;
    }
}