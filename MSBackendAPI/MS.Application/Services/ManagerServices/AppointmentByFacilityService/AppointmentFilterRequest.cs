using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.AppointmentByFacilityService
{
    /// <summary>
    /// Request model used to filter appointments by facility
    /// </summary>
    public class AppointmentFilterRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid FacilityId { get; set; }
        public Guid? DoctorId { get; set; }
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public AppointmentStatus? Status { get; set; }
        public DateTime? Date { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int Page { get; set; } = 1;
        [Range(1, 100, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int Size { get; set; } = 10;
    }
}