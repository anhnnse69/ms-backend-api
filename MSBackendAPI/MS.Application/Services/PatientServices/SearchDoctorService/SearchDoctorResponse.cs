namespace MS.Application.Services.PatientServices.SearchDoctorService
{
    /// <summary>
    /// Represents a single doctor search result item in the paginated list.
    /// </summary>
    public class SearchDoctorResponse
    {
        /// <summary>The unique identifier of the doctor.</summary>
        public Guid Id { get; set; }
        /// <summary>The full name of the doctor.</summary>
        public string FullName { get; set; }
        /// <summary>The avatar URL of the doctor.</summary>
        public string? Avatar { get; set; }
        /// <summary>The number of years of experience the doctor has.</summary>
        public int ExperienceYears { get; set; }
        /// <summary>The average rating score of the doctor.</summary>
        public double AverageRating { get; set; }
        /// <summary>The specialty associated with the doctor.</summary>
        public SearchDoctorSpecialtyResponse Specialty { get; set; }
        /// <summary>The list of facilities the doctor is affiliated with.</summary>
        public List<SearchDoctorFacilityResponse> Facilities { get; set; } = new List<SearchDoctorFacilityResponse>();
    }
}
