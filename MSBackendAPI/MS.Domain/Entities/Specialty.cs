using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class Specialty
    {
        [Key]
        public Guid Id { get; set; }

        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; }
        public string DescriptionEn { get; set; }
        public string IconUrl { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
