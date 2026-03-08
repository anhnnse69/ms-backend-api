using MS.Domain.Enums.Types;

namespace MS.Application.Services.DoctorServices.RejectDoctorAppointmentService
{
    /// <summary>
    /// Response model for rejected appointment
    /// </summary>
    public class RejectDoctorAppointmentResponse
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }
        // Updated appointment status
        public AppointmentStatus Status { get; set; }
    }
}
