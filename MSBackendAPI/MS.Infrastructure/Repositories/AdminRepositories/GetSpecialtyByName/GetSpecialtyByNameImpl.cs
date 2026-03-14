using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.GetSpecialtyByName
{
    /// <summary>
    /// Provides implementation for retrieving a specialty by its Vietnamese name.
    /// </summary>
    public class GetSpecialtyByNameImpl
        : RepositoryQueryBase<Specialty, Guid, AppDbContext>, IGetSpecialtyByName
    {
        /// <summary>
        /// Initializes a new instance of GetSpecialtyByNameImpl.
        /// </summary>
        /// <param name="context">Database context</param>
        public GetSpecialtyByNameImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Executes query to retrieve a specialty by Vietnamese name.
        /// </summary>
        /// <param name="name">Vietnamese name of the specialty</param>
        /// <returns>
        /// The specialty entity if found; otherwise null.
        /// </returns>
        public async Task<Specialty?> Execute(string name)
        {
            return await FindByCondition(
                // compare name ignoring case
                x => x.NameVi.ToLower() == name.ToLower()
                // exclude deleted specialties
                && !x.IsDeleted,
                // no tracking for read-only query
                trackChanges: false)
                // return first matching record
                .FirstOrDefaultAsync();               
        }
    }
}