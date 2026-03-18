using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreateAppointment
{
    /// <summary>
    /// Repository interface for persisting a new appointment to the database.
    /// </summary>
    public interface ICreateAppointmentRepository
    {
        /// <summary>
        /// Persists the specified appointment entity to the database.
        /// </summary>
        /// <param name="appointment">The appointment entity to persist.</param>
        /// <returns>The persisted appointment entity.</returns>
        Task<Appointment> Execute(Appointment appointment);
    }
}
