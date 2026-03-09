using MS.Domain.Enums.Types;

namespace MS.Application.Services.DoctorServices.GetDoctorPatientInfoService
{
    /// <summary>
    /// Response model representing patient information
    /// </summary>
    public class GetDoctorPatientInfoResponse
    {
        // Patient identifier
        public Guid PatientId { get; set; }
        // Display name for UI
        public string DisplayName { get; set; }
        // Full name of the patient
        public string FullName { get; set; }
        // Date of birth of the patient
        public DateTimeOffset DateOfBirth { get; set; }
        // Gender of the patient
        public Gender Gender { get; set; }
        // Contact phone number
        public string PhoneNumber { get; set; }
        // Email address
        public string Email { get; set; }
        // Residential address
        public string Address { get; set; }
        // Identity card number
        public string IdentityCard { get; set; }
        // Health insurance number
        public string InsuranceNumber { get; set; }
        // Avatar URL for UI display
        public string AvatarUrl { get; set; }
    }
}