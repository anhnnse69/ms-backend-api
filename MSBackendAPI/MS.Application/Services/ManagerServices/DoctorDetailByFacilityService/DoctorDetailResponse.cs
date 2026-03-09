namespace MS.Application.Services.DoctorDetailByFacilityService
{
    /// <summary>
    /// Response model containing doctor detail information
    /// </summary>
    public class DoctorDetailResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string BioVi { get; set; }
        public string AcademicTitleVi { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string SpecialtyName { get; set; }
        public List<string> Languages { get; set; }
    }
}
