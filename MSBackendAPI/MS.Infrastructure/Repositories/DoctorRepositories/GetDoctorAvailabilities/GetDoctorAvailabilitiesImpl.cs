using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    /// <summary>
    /// Repository implementation used to retrieve doctor availability schedule
    /// </summary>
    public class GetDoctorAvailabilitiesImpl : RepositoryQueryBase<DoctorAvailability, Guid, AppDbContext>, IGetDoctorAvailabilities
    {
        /// <summary>
        /// Constructor for GetDoctorAvailabilities repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetDoctorAvailabilitiesImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve availability list of a doctor
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Collection of doctor availability entities</returns>
        public async Task<IEnumerable<DoctorAvailability>> Execute(Guid doctorId)
        {
            return await FindByCondition(x => x.DoctorId == doctorId, false)
                // Include facility information related to availability
                .Include(x => x.Facility)
                .ToListAsync();
        }
    }
}