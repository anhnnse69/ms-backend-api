using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.CreateDoctor
{
    /// <summary>
    /// Defines the contract for creating a new doctor record.
    /// </summary>
    public interface ICreateDoctor
    {
        /// <summary>
        /// Persists a doctor entity to the database.
        /// </summary>
        /// <param name="doctor">Doctor entity to be created.</param>
        /// <returns>The created <see cref="Doctor"/> entity.</returns>
        Task<Doctor> Execute(Doctor doctor);
    }
}