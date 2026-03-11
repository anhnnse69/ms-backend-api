using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.AdminServices.CreateFacilityService
{
    /// <summary>
    /// Request object for creating a facility.
    /// </summary>
    public class CreateFacilityRequest
    {
        /// <summary>Vietnamese name of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameVi { get; set; } = string.Empty;

        /// <summary>English name of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameEn { get; set; } = string.Empty;

        /// <summary>Vietnamese description of the facility (optional).</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string DescriptionVi { get; set; } = string.Empty;

        /// <summary>English description of the facility (optional).</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string DescriptionEn { get; set; } = string.Empty;

        /// <summary>Logo image URL (optional).</summary>
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string LogoUrl { get; set; } = string.Empty;

        /// <summary>Address of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(300, MinimumLength = 5, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Address { get; set; } = string.Empty;

        /// <summary>Contact phone number of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string Phone { get; set; } = string.Empty;

        /// <summary>Contact email of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(150, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; } = string.Empty;

        /// <summary>City where the facility is located.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string City { get; set; } = string.Empty;

        /// <summary>Type of facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EnumDataType(typeof(FacilityType), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public FacilityType Type { get; set; }
    }
}