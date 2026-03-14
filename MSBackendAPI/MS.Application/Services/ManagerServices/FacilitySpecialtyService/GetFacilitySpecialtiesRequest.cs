using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.FacilitySpecialtyService
{
    public class GetFacilitySpecialtiesRequest
    {
        /// <summary>
        /// ID của cơ sở y tế cần xem chuyên khoa
        /// </summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid FacilityId { get; set; }
    }
}
