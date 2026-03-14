using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.CreateSpecialty
{
    /// <summary>
    /// Provides implementation for creating a specialty entity in the database.
    /// </summary>
    public class CreateSpecialtyImpl
        : RepositoryBase<Specialty, Guid, AppDbContext>, ICreateSpecialty
    {
        /// <summary>
        /// Initializes a new instance of CreateSpecialtyImpl.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="unitOfWork">Unit of work instance</param>
        public CreateSpecialtyImpl( AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Insert a new specialty record into the database.
        /// </summary>
        /// <param 
        /// name="specialty">Specialty entity to be created
        /// </param>
        /// <returns>
        /// The created specialty entity
        /// </returns>
        public async Task<Specialty> Execute(Specialty specialty)
        {
            // Add specialty entity to DbContext
            await CreateAsync(specialty);
            // Save changes to database
            await SaveChangesAsync();
            // Return created entity
            return specialty;
        }
    }
}