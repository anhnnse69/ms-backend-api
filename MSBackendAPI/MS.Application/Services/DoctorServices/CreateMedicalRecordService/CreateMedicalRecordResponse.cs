namespace MS.Application.Services.DoctorServices.CreateMedicalRecordService
{
    /// <summary>
    /// Response model representing created medical record information
    /// </summary>
    public class CreateMedicalRecordResponse
    {
        // Patient full name
        public string PatientName { get; set; } = string.Empty;
        // Patient date of birth
        public string PatientDateOfBirth { get; set; } = string.Empty;
        // Patient gender
        public string PatientGender { get; set; } = string.Empty;
        // Patient phone number
        public string? PatientPhoneNumber { get; set; }
        // Doctor full name
        public string DoctorName { get; set; } = string.Empty;
        // Doctor specialty name
        public string? DoctorSpecialty { get; set; }
        // Appointment date
        public string AppointmentDate { get; set; } = string.Empty;
        // Facility name where appointment takes place
        public string FacilityName { get; set; } = string.Empty;
        // Appointment status
        public string AppointmentStatus { get; set; } = string.Empty;
        // Patient symptoms
        public string Symptoms { get; set; } = string.Empty;
        // Doctor diagnosis
        public string Diagnosis { get; set; } = string.Empty;
        // Additional doctor notes
        public string? Notes { get; set; }
    }
}