using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorDetailByFacility
{
    /// <summary>
    /// Repository implementation for retrieving doctor detail information
    /// </summary>
    public class GetDoctorDetailByFacilityImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorDetailByFacility
    {
        /// <summary>
        /// Initializes a new instance of the repository
        /// </summary>
        /// <param name="context">Database context</param>
        public GetDoctorDetailByFacilityImpl(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        /// Retrieve doctor detail by identifier
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Doctor entity with related information</returns>
        public async Task<Doctor?> Execute(Guid doctorId)
        {
            return await FindByCondition(
                    d => d.Id == doctorId,
                    trackChanges: false)
                .Include(d => d.Specialty)
                .Include(d => d.Languages)
                .Include(d => d.Availabilities)
                .FirstOrDefaultAsync();
        }
    }
}