namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents the many-to-many relationship between Facility and Specialty,
    /// indicating which specialties are offered at which facilities.
    /// This allows flexible management of specialties at each medical facility.
    /// </summary>
    public class FacilitySpecialty
    {
        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
