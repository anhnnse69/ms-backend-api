using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.AdminServices.UpdateSpecialtyService
{
    /// <summary>
    /// Represents the request data required to update a specialty.
    /// </summary>
    public class UpdateSpecialtyRequest
    {
        /// <summary>The Vietnamese name of the specialty.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameVi { get; set; } = string.Empty;

        /// <summary>The English name of the specialty.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(200, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameEn { get; set; } = string.Empty;

        /// <summary>Description in Vietnamese.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string DescriptionVi { get; set; } = string.Empty;

        /// <summary>Description in English.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string DescriptionEn { get; set; } = string.Empty;

        /// <summary>The icon URL of the specialty.</summary>
        [Url(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [StringLength(500, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string? IconUrl { get; set; }
    }
}