using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Roles;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.AdminServices.CreateUser
{
    /// <summary>
    /// Request object for create user.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>The email address of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(150, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; } = string.Empty;

        /// <summary>The password used for the user account.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PasswordStrength(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 8, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Password { get; set; } = string.Empty;

        /// <summary>The full name of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string FullName { get; set; } = string.Empty;

        /// <summary>The avatar URL representing the user's profile image (optional).</summary>
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>The phone number of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>The system role assigned to the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EnumDataType(typeof(SystemRole), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4022))]
        public SystemRole Role { get; set; }
    }
}