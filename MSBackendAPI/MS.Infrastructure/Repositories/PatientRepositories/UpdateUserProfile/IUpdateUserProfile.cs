using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateUserProfile
{
    /// <summary>
    /// Repository interface for persisting user profile updates to the database.
    /// </summary>
    public interface IUpdateUserProfile
    {
        /// <summary>
        /// Persists the updated user entity to the database.
        /// </summary>
        /// <param name="user">The user entity containing updated profile data.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task Execute(User user);
    }
}
