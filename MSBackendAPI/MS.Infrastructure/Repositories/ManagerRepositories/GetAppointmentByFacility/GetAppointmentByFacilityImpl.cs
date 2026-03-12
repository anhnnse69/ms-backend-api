using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByFacility
{
    /// <summary>
    /// Repository implementation for retrieving appointments by facility
    /// </summary>
    public class GetAppointmentByFacilityImpl
        : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentByFacility
    {
        /// <summary>
        /// Initializes a new instance of the repository
        /// </summary>
        /// <param name="context">
        /// Database context used to access appointment data
        /// </param>
        public GetAppointmentByFacilityImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve appointment query filtered by facility identifier
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <returns>
        /// IQueryable collection of appointments belonging to the specified facility,
        /// including related doctor and patient information
        /// </returns>
        public IQueryable<Appointment> Execute(Guid facilityId)
        {
            return FindByCondition(
                    a => a.FacilityId == facilityId && !a.IsDeleted,
                    false)
                .Include(a => a.Doctor)
                .Include(a => a.Patient);
        }
    }
}