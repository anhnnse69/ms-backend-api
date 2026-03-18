using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Domain.Enums.Roles;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetFacilityIdByManagerId
{
    /// <summary>
    /// Repository implementation for retrieving the facility ID of a manager.
    /// </summary>
    public class GetFacilityIdByManagerIdImpl : RepositoryQueryBase<User, Guid, AppDbContext>, IGetFacilityIdByManagerId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityIdByManagerIdImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetFacilityIdByManagerIdImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the facility ID associated with the specified manager.
        /// </summary>
        /// <param name="managerId">The manager identifier.</param>
        /// <returns>The facility ID if the user exists and is a manager; otherwise, null.</returns>
        public async Task<Guid?> Execute(Guid managerId)
        {
            var user = await FindByCondition(u => u.Id == managerId && u.Role == SystemRole.Manager, false)
                .Select(u => u.FacilityId)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
