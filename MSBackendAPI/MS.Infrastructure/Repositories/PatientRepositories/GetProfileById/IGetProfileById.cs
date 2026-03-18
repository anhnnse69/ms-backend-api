using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetProfileById
{
    /// <summary>
    /// Repository interface for retrieving a user by their unique identifier.
    /// </summary>
    public interface IGetProfileById
    {
        /// <summary>
        /// Retrieves the user matching the specified identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><see cref="User"/> if found; otherwise null.</returns>
        Task<User?> Execute(Guid userId);
    }
}
