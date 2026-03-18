namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Represents the detailed profile response of a doctor.
    /// </summary>
    public class GetDoctorDetailResponse
    {
        /// <summary>The unique identifier of the doctor.</summary>
        public Guid Id { get; set; }
        /// <summary>The full name of the doctor.</summary>
        public string FullName { get; set; }
        /// <summary>The avatar URL of the doctor.</summary>
        public string? Avatar { get; set; }
        /// <summary>The biography or introduction of the doctor.</summary>
        public string? Biography { get; set; }
        /// <summary>The number of years of experience the doctor has.</summary>
        public int ExperienceYears { get; set; }
        /// <summary>The average rating score of the doctor.</summary>
        public double AverageRating { get; set; }
        /// <summary>The list of specialties the doctor belongs to.</summary>
        public List<DoctorSpecialtyResponse> Specialties { get; set; } = new List<DoctorSpecialtyResponse>();
        /// <summary>The list of facilities the doctor is affiliated with.</summary>
        public List<DoctorFacilityResponse> Facilities { get; set; } = new List<DoctorFacilityResponse>();
        /// <summary>The list of weekly availability schedules of the doctor.</summary>
        public List<DoctorAvailabilityResponse> Availabilities { get; set; } = new List<DoctorAvailabilityResponse>();
    }
}
