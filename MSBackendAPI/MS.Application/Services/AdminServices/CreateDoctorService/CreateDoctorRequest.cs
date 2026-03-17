using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.AdminServices.CreateDoctorService
{
    /// <summary>
    /// Request object for creating a doctor.
    /// </summary>
    public class CreateDoctorRequest
    {
        /// <summary>The associated user id.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid UserId { get; set; }
        /// <summary>The full name of the doctor.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string FullName { get; set; } = string.Empty;
        /// <summary>The email of the doctor.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(150, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; } = string.Empty;
        /// <summary>The phone number of the doctor.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>The avatar URL.</summary>
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string AvatarUrl { get; set; } = string.Empty;
        /// <summary>The photo URL.</summary>
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string PhotoUrl { get; set; } = string.Empty;
        /// <summary>Biography in Vietnamese.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string BioVi { get; set; } = string.Empty;
        /// <summary>Biography in English.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string BioEn { get; set; } = string.Empty;
        /// <summary>Academic title in Vietnamese.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string AcademicTitleVi { get; set; } = string.Empty;
        /// <summary>Academic title in English.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string AcademicTitleEn { get; set; } = string.Empty;
        /// <summary>Years of experience.</summary>
        [Range(0, 60, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int YearsOfExperience { get; set; }
        /// <summary>The specialty id.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid SpecialtyId { get; set; }
    }
}