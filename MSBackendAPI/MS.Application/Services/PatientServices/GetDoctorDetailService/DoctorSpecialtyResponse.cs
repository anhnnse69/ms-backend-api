namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Represents a specialty associated with a doctor.
    /// </summary>
    public class DoctorSpecialtyResponse
    {
        /// <summary>The unique identifier of the specialty.</summary>
        public Guid Id { get; set; }
        /// <summary>The name of the specialty.</summary>
        public string Name { get; set; }
    }
}
