using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.UpdateFacilityService
{
    /// <summary>
    /// Represents the request data required for a manager to update a facility.
    /// </summary>
    public class UpdateManagerFacilityRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid Id { get; set; }
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameVi { get; set; } = string.Empty;
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameEn { get; set; } = string.Empty;
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string DescriptionVi { get; set; } = string.Empty;
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string DescriptionEn { get; set; } = string.Empty;
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string? LogoUrl { get; set; }
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [PhoneNumber(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4001))]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [EmailAddress(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EnumDataType(typeof(FacilityType), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public FacilityType Type { get; set; }
    }
}