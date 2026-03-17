using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateDoctor
{
    /// <summary>
    /// Defines the contract for updating doctor data.
    /// </summary>
    public interface IUpdateDoctor
    {
        /// <summary>
        /// Updates the specified doctor entity.
        /// </summary>
        /// <param name="doctor">
        /// The doctor entity containing updated information.
        /// </param>
        /// <returns>
        /// The updated <see cref="Doctor"/> entity after persistence.
        /// </returns>
        Task<Doctor> Execute(Doctor doctor);
    }
}