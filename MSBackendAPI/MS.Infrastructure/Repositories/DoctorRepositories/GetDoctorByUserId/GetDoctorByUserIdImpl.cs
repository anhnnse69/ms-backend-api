using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId
{
    /// <summary>
    /// Repository implementation used to retrieve doctor entity by user identifier
    /// </summary>
    public class GetDoctorByUserIdImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorByUserId
    {

        /// <summary>
        /// Constructor for GetDoctorByUserId repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetDoctorByUserIdImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve doctor entity by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity associated with the user</returns>
        public async Task<Doctor> Execute(Guid userId)
        {
            return await FindByCondition(x => x.UserId == userId, false)
                .FirstOrDefaultAsync();
        }
    }
}
