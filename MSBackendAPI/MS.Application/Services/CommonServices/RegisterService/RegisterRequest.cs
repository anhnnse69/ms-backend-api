    using MS.Application.Common.Attributes;
    using MS.Domain.Enums.GeneralCodes;
    using MS.Domain.Enums.Types;
    using System.ComponentModel.DataAnnotations;

    namespace MS.Application.Services.CommonServices.RegisterService
    {
        /// <summary>
        /// Request object for patient registration.
        /// </summary>
        public class RegisterRequest
        {
            /// <summary>Full name of the patient.</summary>
            [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
            [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
            [StringLength(150, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
            public string FullName { get; set; } = string.Empty;

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
            [PasswordStrength(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
            [StringLength(100, MinimumLength = 8, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
            public string Password { get; set; } = string.Empty;

            /// <summary>Phone number of the patient.</summary>
            [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
            [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
            [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
            public string PhoneNumber { get; set; } = string.Empty;

            /// <summary>Date of birth of the patient.</summary>
            [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            public DateTimeOffset DateOfBirth { get; set; }

            /// <summary>Gender of the patient.</summary>
            [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
            public Gender Gender { get; set; }
        }
    }