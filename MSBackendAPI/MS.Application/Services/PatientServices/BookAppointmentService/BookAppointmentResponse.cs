namespace MS.Application.Services.PatientServices.BookAppointmentService
{
    /// <summary>
    /// Represents the response returned after successfully booking an appointment.
    /// </summary>
    public class BookAppointmentResponse
    {
        /// <summary>The unique identifier of the newly created appointment.</summary>
        public Guid Id { get; set; }
        /// <summary>The current status of the appointment.</summary>
        public string Status { get; set; } = string.Empty;
    }
}
