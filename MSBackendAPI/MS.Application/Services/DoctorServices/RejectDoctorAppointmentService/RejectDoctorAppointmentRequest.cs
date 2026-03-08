namespace MS.Application.Services.DoctorServices.RejectDoctorAppointmentService
{
    /// <summary>
    /// Request model for rejecting doctor appointment
    /// </summary>
    public class RejectDoctorAppointmentRequest
    {
        // Appointment identifier
        public Guid AppointmentId { get; set; }
        // Reason for rejecting the appointment
        public string Reason { get; set; }
    }
}
