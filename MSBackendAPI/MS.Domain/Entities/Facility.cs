using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a medical facility (hospital, clinic, etc.)
    /// </summary>
    public class Facility : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; } // UI
        public string DescriptionEn { get; set; } // UI
        public string LogoUrl { get; set; } // UI logo
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public FacilityType Type { get; set; }
        public bool IsActive { get; set; }

        public ICollection<DoctorFacility> DoctorFacilities { get; set; } = new List<DoctorFacility>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}