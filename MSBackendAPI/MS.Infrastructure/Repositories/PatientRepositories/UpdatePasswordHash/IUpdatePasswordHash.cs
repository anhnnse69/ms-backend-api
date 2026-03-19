using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdatePasswordHash
{
    /// <summary>
    /// Repository interface for updating a user's password hash in the database.
    /// </summary>
    public interface IUpdatePasswordHash
    {
        /// <summary>
        /// Updates the password hash for the specified user and persists the change.
        /// </summary>
        /// <param name="user">The user entity whose password hash will be updated.</param>
        /// <param name="passwordHash">The new BCrypt password hash to store.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Execute(User user, string passwordHash);
    }
}
