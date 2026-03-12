using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.AppointmentByFacilityService
{
    /// <summary>
    /// Request model used to filter appointments by facility
    /// </summary>
    public class AppointmentFilterRequest
    {
        [Required]
        public Guid FacilityId { get; set; }
        public Guid? DoctorId { get; set; }
        [EnumDataType(typeof(AppointmentStatus))]
        public int? Status { get; set; }
        public DateTime? Date { get; set; }
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;
        [Range(1, 100)]
        public int Size { get; set; } = 10;
    }
}