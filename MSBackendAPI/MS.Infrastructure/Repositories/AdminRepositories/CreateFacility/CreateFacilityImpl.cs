using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.CreateFacility
{
    /// <summary>
    /// Provides an implementation of the ICreateFacility interface
    /// for inserting a new facility into the database.
    /// </summary>
    public class CreateFacilityImpl : RepositoryBase<Facility, Guid, AppDbContext>, ICreateFacility
    {
        /// <summary>
        /// Initializes a new instance of the CreateFacilityImpl class.
        /// </summary>
        /// <param name="context">
        /// Database context used to access facility data.
        /// </param>
        /// <param name="unitOfWork">
        /// Unit of work responsible for handling transactions.
        /// </param>
        public CreateFacilityImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) 
        { }

        /// <summary>
        /// Inserts a new facility record into the database.
        /// </summary>
        /// <param name="facility">
        /// The facility entity to be created.
        /// </param>
        /// <returns>
        /// The created <see cref="Facility"/> entity.
        /// </returns>
        public async Task<Facility> Execute(Facility facility)
        {
            await CreateAsync(facility);
            await SaveChangesAsync();
            return facility;
        }
    }
}