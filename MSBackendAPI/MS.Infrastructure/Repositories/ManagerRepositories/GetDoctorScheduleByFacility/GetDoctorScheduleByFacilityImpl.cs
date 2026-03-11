using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorScheduleByFacility
{
    /// <summary>
    /// Repository implementation for retrieving doctor schedules by facility
    /// </summary>
    public class GetDoctorScheduleByFacilityImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorScheduleByFacility
    {
        /// <summary>
        /// Initializes a new instance of the repository
        /// </summary>
        /// <param name="context">Database context</param>
        public GetDoctorScheduleByFacilityImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve doctors and their schedules for a specific facility
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// Tuple containing the list of doctors with schedules and the total record count
        /// </returns>
        public async Task<(List<Doctor>, int)> Execute(Guid facilityId, int page, int size)
        {
            var query = FindByCondition(
                    d => d.Facilities.Any(f => f.FacilityId == facilityId),
                    false)
                .Include(d => d.Availabilities
                    .Where(a => a.FacilityId == facilityId && a.IsDeleted))
                .Where(d => d.IsDeleted);
            var total = await query.CountAsync();
            var doctors = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return (doctors, total);
        }
    }
}