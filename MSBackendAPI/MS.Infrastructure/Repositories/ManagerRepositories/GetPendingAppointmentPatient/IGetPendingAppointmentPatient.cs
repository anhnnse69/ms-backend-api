using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetPendingAppointmentPatient
{
    /// <summary>
    /// Repository interface for retrieving pending patient appointments
    /// </summary>
    public interface IGetPendingAppointmentPatient
    {
        /// <summary>
        /// Execute query to retrieve a paginated list of pending appointments
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// A tuple containing:
        /// - List of appointment entities
        /// - Total number of records matching the query
        /// </returns>
        Task<(List<Appointment> Appointments, int Total)> Execute(int page, int size);
    }
}