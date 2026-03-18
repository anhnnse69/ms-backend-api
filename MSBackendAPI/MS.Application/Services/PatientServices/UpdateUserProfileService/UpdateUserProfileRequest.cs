using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.UpdateUserProfileService
{
    /// <summary>
    /// Request model for updating the authenticated user's personal profile.
    /// </summary>
    public class UpdateUserProfileRequest
    {
        /// <summary>The updated display name of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string DisplayName { get; set; }
        /// <summary>The updated full name of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string FullName { get; set; }
        /// <summary>The updated phone number of the user.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
        public string PhoneNumber { get; set; }
        /// <summary>The updated avatar URL of the user.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string? AvatarUrl { get; set; }
    }
}
