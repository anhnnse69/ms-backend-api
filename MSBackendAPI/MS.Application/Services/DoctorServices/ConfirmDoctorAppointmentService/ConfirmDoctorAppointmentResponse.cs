using MS.Domain.Enums.Types;

namespace MS.Application.Services.DoctorServices.ConfirmDoctorAppointmentService
{
    /// <summary>
    /// Response model for confirmed appointment
    /// </summary>
    public class ConfirmDoctorAppointmentResponse
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }
        // Updated appointment status
        public AppointmentStatus Status { get; set; }
    }
}
