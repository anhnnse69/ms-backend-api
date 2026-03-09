namespace MS.Application.Services.DoctorsByFacilityService
{
    /// <summary>
    /// Response model representing doctor information in the doctor list
    /// </summary>
    public class DoctorListResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string AcademicTitleVi { get; set; }
        public string AvatarUrl { get; set; }
        public string SpecialtyName { get; set; }
        public int YearsOfExperience { get; set; }
        public double AverageRating { get; set; }
    }
}
