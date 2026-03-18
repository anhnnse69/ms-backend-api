using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentsForReportAsync
{
    /// <summary>
    /// Repository interface for retrieving appointment data used in facility performance report.
    /// </summary>
    public interface IGetFacilityPerformanceReport
    {
        /// <summary>
        /// Retrieves a list of appointments matching the specified criteria.
        /// </summary>
        /// <param name="facilityId">The facility identifier (required).</param>
        /// <param name="startDate">Optional start date for filtering appointments (inclusive).</param>
        /// <param name="endDate">Optional end date for filtering appointments (inclusive).</param>
        /// <param name="doctorId">Optional doctor identifier to filter results for a specific doctor.</param>
        /// <returns>List of appointments with related doctor data.</returns>
        Task<List<Appointment>> Execute(Guid facilityId, DateTime? startDate, DateTime? endDate, Guid? doctorId);
    }
}