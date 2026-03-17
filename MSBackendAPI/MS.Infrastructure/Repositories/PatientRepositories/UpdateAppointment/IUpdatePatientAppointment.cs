using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateAppointment
{
    /// <summary>
    /// Interface for updating an appointment
    /// </summary>
    public interface IUpdatePatientAppointment
    {
        /// <summary>
        /// Execute logic to update an appointment
        /// </summary>
        /// <param name="appointment">The appointment entity to update</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task Execute(Appointment appointment);
    }
}
