using MS.Domain.Enums.Types;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Request model for updating appointment status
    /// </summary>
    public class UpdateDoctorAppointmentStatusRequest
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }

        // New appointment status
        public AppointmentStatus Status { get; set; }
    }
}
