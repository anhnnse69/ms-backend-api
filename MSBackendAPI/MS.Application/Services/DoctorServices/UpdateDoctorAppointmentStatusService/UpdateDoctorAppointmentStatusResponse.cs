using MS.Domain.Enums.Types;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Response model for updated appointment
    /// </summary>
    public class UpdateDoctorAppointmentStatusResponse
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }
        // Updated appointment status
        public AppointmentStatus Status { get; set; }
    }
}
