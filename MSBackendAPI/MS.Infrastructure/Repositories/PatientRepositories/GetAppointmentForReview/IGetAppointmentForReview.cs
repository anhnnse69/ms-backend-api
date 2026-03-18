using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetAppointmentForReview
{
    /// <summary>
    /// Repository interface for retrieving an appointment by its identifier for review submission.
    /// </summary>
    public interface IGetAppointmentForReview
    {
        /// <summary>
        /// Retrieves the appointment entity matching the specified identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>Appointment entity if found; otherwise null.</returns>
        Task<Appointment?> Execute(Guid appointmentId);
    }
}
