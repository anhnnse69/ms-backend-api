using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorByFacility
{
    /// <summary>
    /// Repository implementation for retrieving doctors by facility
    /// </summary>
    public class GetDoctorByFacilityImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorByFacility
    {
        /// <summary>
        /// Initializes a new instance of the repository with database context
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetDoctorByFacilityImpl(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        /// Retrieves active doctors associated with a specific facility
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// A tuple containing the paginated list of doctors and total number of records
        /// </returns>
        public async Task<(List<Doctor> Doctors, int Total)> Execute(Guid facilityId, int page, int size)
        {
            // 1. Build query to filter doctors by facility (global soft-delete filter excludes deleted records automatically)
            var query = FindByCondition(
                    d => d.Facilities.Any(f => f.FacilityId == facilityId),
                    trackChanges: false)
                .Include(d => d.Specialty);
            // 2. Retrieve total number of matching records
            var total = await query.CountAsync();
            // 3. Apply pagination and retrieve doctor list
            var doctors = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            // 4. Return result tuple
            return (doctors, total);
        }
    }
}