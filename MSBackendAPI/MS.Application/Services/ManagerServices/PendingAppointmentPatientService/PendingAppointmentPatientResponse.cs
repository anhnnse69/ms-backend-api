using MS.Domain.Enums.Types;

namespace MS.Application.Services.ManagerServices.PendingAppointmentPatientService
{
    /// <summary>
    /// Response model representing a pending appointment booked by a patient
    /// </summary>
    public class PendingAppointmentResponse
    {
        public Guid AppointmentId { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string DoctorName { get; set; }
        public string FacilityName { get; set; }
        public string SpecialtyName { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
