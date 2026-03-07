using MS.Application.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.CommonServices.LoginService
{
    /// <summary>
    /// Request object for user login.
    /// </summary>
    public class LoginRequest
    {
        // email_address
        [Required(ErrorMessage = "APP_MESSAGE_0400")]
        [NotBlank(ErrorMessage = "APP_MESSAGE_0401")]
        [EmailAddress(ErrorMessage = "APP_MESSAGE_0405")]
        public string EmailAddress { get; set; } = string.Empty;

        // password
        [Required(ErrorMessage = "APP_MESSAGE_0400")]
        [NotBlank(ErrorMessage = "APP_MESSAGE_0401")]
        public string Password { get; set; } = string.Empty;
    }
}
