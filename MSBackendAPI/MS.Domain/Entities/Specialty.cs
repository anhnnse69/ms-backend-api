using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class Specialty : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; }
        public string DescriptionEn { get; set; }
        public string IconUrl { get; set; } // UI icon

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
