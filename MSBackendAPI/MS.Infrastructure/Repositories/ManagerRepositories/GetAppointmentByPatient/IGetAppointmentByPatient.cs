using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByPatient
{
    /// <summary>
    /// Contract for retrieving appointment by patient ID and appointment ID
    /// </summary>
    public interface IGetAppointmentByPatient
    {
        /// <summary>
        /// Get an appointment by patient ID and appointment ID
        /// </summary>
        /// <param name="patientId">The patient ID</param>
        /// <param name="appointmentId">The appointment ID</param>
        /// <returns>The appointment entity or null if not found</returns>
        Task<Appointment?> Execute(Guid patientId, Guid appointmentId);
    }
}
