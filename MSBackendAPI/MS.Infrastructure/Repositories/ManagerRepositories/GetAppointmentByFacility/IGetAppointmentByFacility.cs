using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByFacility
{
    /// <summary>
    /// Repository contract for retrieving appointments by facility
    /// </summary>
    public interface IGetAppointmentByFacility
    {
        /// <summary>
        /// Retrieve appointments filtered by facility identifier
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <returns>
        /// IQueryable collection of appointments belonging to the specified facility
        /// </returns>
        IQueryable<Appointment> Execute(Guid facilityId);
    }
}