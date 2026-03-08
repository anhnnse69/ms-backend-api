using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdatePassword
{
    /// <summary>
    /// Interface for updating user password
    /// </summary>
    public interface IUpdatePassword
    {
        /// <summary>
        /// Update password of user
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns></returns>
        Task Execute(User user);
    }
}