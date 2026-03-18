namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Represents a facility that a doctor is affiliated with.
    /// </summary>
    public class DoctorFacilityResponse
    {
        /// <summary>The unique identifier of the facility.</summary>
        public Guid Id { get; set; }
        /// <summary>The name of the facility.</summary>
        public string Name { get; set; }
    }
}
