using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByPhone
{
    /// <summary>
    /// Provides an implementation of the IGetFacilityByPhone interface for retrieving
    /// facility information by phone number from the application's data store.
    /// </summary>
    public class GetFacilityByPhoneImpl
        : RepositoryQueryBase<Facility, Guid, AppDbContext>, IGetFacilityByPhone
    {
        /// <summary>
        /// Initializes a new instance of the GetFacilityByPhoneImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used to access facility data. Cannot be null.
        /// </param>
        public GetFacilityByPhoneImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a facility by the specified phone number.
        /// </summary>
        /// <param name="phone">The phone number of the facility.</param>
        /// <returns>
        /// A <see cref="Facility"/> entity if a facility with the specified phone number exists; otherwise, <c>null</c>.
        /// </returns>
        public async Task<Facility> Execute(string phone)
        {
            return await FindByCondition(
                x => x.Phone == phone,
                trackChanges: false
            ).FirstOrDefaultAsync();
        }
    }
}