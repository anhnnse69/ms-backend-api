namespace MS.Domain.Entities
{
    /// <summary>
    /// Many-to-many relationship between Doctor and Facility
    /// </summary>
    public class DoctorFacility
    {
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        public DateTime AssignedDate { get; set; }
        public bool IsPrimary { get; set; }
    }
}
