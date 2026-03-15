using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateSpecialty
{
    /// <summary>
    /// Defines the contract for updating a specialty.
    /// </summary>
    public interface IUpdateSpecialty
    {
        /// <summary>
        /// Updates specialty in database.
        /// </summary>
        Task Execute(Specialty specialty);
    }
}