using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.CreateSpecialty
{
    /// <summary>
    /// Defines the contract for creating a specialty.
    /// </summary>
    public interface ICreateSpecialty
    {
        /// <summary>
        /// Inserts a new specialty into the database.
        /// </summary>
        /// <param name="specialty">
        /// The specialty entity to be created.
        /// </param>
        /// <returns>
        /// The created <see cref="Specialty"/> entity.
        /// </returns>
        Task<Specialty> Execute(Specialty specialty);
    }
}