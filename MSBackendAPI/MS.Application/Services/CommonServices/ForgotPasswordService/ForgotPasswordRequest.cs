using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.ForgotPasswordService
{
    /// <summary>
    /// Represents the request payload for initiating a password reset.
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>The email address associated with the user account.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; }
    }
}
