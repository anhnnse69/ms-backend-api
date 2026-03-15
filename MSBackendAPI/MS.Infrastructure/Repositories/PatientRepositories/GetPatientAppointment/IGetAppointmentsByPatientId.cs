using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientAppointment
{
    /// <summary>
    /// Interface for retrieving appointments by patient ID
    /// </summary>
    public interface IGetAppointmentsByPatientId
    {
        /// <summary>
        /// Execute query to get appointments
        /// </summary>
        /// <param name="patientId">The ID of the patient</param>
        /// <returns>List of appointments</returns>
        Task<List<Appointment>> Execute(Guid patientId);
    }
}
