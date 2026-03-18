namespace MS.Application.Services.PatientServices.SearchDoctorService
{
    /// <summary>
    /// Represents the specialty information in a doctor search result.
    /// </summary>
    public class SearchDoctorSpecialtyResponse
    {
        /// <summary>The unique identifier of the specialty.</summary>
        public Guid Id { get; set; }
        /// <summary>The name of the specialty.</summary>
        public string Name { get; set; }
    }
}
