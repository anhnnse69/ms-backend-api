using MS.Domain.Enums.Types;

namespace MS.Application.Services.Doctors.GetDoctorAppointmentService
{
    /// <summary>
    /// Response model for doctor appointment
    /// </summary>
    public class GetDoctorAppointmentResponse
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }

        // Patient full name
        public string PatientName { get; set; }

        // Facility name where the appointment takes place
        public string FacilityName { get; set; }

        // Appointment scheduled time
        public DateTimeOffset AppointmentTime { get; set; }

        // Current status of the appointment
        public AppointmentStatus Status { get; set; }

        // Notes of the appointment
        public string? Notes { get; set; }
    }
}
