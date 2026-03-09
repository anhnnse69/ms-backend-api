using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByEmail
{
    /// <summary>
    /// Provides an implementation of the IGetFacilityByEmail interface for retrieving
    /// facility information by email address from the application's data store.
    /// </summary>
    public class GetFacilityByEmailImpl
        : RepositoryQueryBase<Facility, Guid, AppDbContext>, IGetFacilityByEmail
    {
        /// <summary>
        /// Initializes a new instance of the GetFacilityByEmailImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used to access facility data. Cannot be null.
        /// </param>
        public GetFacilityByEmailImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a facility by the specified email address.
        /// </summary>
        /// <param name="email">The email address of the facility.</param>
        /// <returns>
        /// A <see cref="Facility"/> entity if a facility with the specified email exists; otherwise, <c>null</c>.
        /// </returns>
        public async Task<Facility> Execute(string email)
        {
            return await FindByCondition(
                x => x.Email == email,
                trackChanges: false
            ).FirstOrDefaultAsync();
        }
    }
}