using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentReport
{
    /// <summary>
    /// Repository interface for retrieving appointment data used in report generation.
    /// </summary>
    public interface IGetAppointmentReport
    {
        /// <summary>
        /// Retrieves the base query for appointments used in report calculation.
        /// Only non-deleted appointments should be returned.
        /// </summary>
        /// <returns>
        /// IQueryable collection of <see cref="Appointment"/> entities
        /// that can be further filtered by the service layer.
        /// </returns>
        IQueryable<Appointment> Execute();
    }
}