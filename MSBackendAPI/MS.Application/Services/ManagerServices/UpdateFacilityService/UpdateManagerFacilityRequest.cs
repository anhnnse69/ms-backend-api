using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.UpdateFacilityService
{
    /// <summary>
    /// Request object for updating facility information.
    /// </summary>
    public class UpdateManagerFacilityRequest
    {
        /// <summary>Facility identifier.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid Id { get; set; }
        /// <summary>Vietnamese name of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameVi { get; set; } = string.Empty;
        /// <summary>English name of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [StringLength(200, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string NameEn { get; set; } = string.Empty;
        /// <summary>Vietnamese description.</summary>
        public string DescriptionVi { get; set; } = string.Empty;
        /// <summary>English description.</summary>
        public string DescriptionEn { get; set; } = string.Empty;
        /// <summary>Logo URL.</summary>
        public string LogoUrl { get; set; } = string.Empty;
        /// <summary>Facility address.</summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>Contact phone.</summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>Contact email.</summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>City where the facility is located.</summary>
        public string City { get; set; } = string.Empty;
    }
}