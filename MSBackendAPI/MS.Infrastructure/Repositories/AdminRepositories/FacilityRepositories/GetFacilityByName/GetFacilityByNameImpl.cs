using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByName
{
    /// <summary>
    /// Provides an implementation of the IGetFacilityByName interface for retrieving
    /// facility information by Vietnamese name from the application's data store.
    /// </summary>
    public class GetFacilityByNameImpl
        : RepositoryQueryBase<Facility, Guid, AppDbContext>, IGetFacilityByName
    {
        /// <summary>
        /// Initializes a new instance of the GetFacilityByNameImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used to access facility data. Cannot be null.
        /// </param>
        public GetFacilityByNameImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a facility by the specified Vietnamese name.
        /// </summary>
        /// <param name="name">The Vietnamese name of the facility.</param>
        /// <returns>
        /// A <see cref="Facility"/> entity if a facility with the specified name exists; otherwise, <c>null</c>.
        /// </returns>
        public async Task<Facility> Execute(string name)
        {
            return await FindByCondition(
                x => x.NameVi == name,
                trackChanges: false
            ).FirstOrDefaultAsync();
        }
    }
}