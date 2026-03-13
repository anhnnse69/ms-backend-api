namespace MS.Application.Services.DoctorServices.CreateMedicalRecordService
{
    /// <summary>
    /// Response model representing created medical record information
    /// </summary>
    public class CreateMedicalRecordResponse
    {
        // Patient full name
        public string PatientName { get; set; }
        // Patient date of birth
        public string PatientDateOfBirth { get; set; }
        // Patient gender
        public string PatientGender { get; set; }
        // Patient phone number
        public string? PatientPhoneNumber { get; set; }
        // Doctor full name
        public string DoctorName { get; set; }
        // Doctor specialty name
        public string? DoctorSpecialty { get; set; }
        // Appointment date
        public string AppointmentDate { get; set; }
        // Facility name where appointment takes place
        public string FacilityName { get; set; }
        // Appointment status
        public string AppointmentStatus { get; set; }
        // Patient symptoms
        public string Symptoms { get; set; }
        // Doctor diagnosis
        public string Diagnosis { get; set; }
        // Additional doctor notes
        public string? Notes { get; set; }
    }
}