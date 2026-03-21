using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteByTarget
{
    /// <summary>
    /// Repository interface for retrieving an existing favorite record including soft-deleted ones.
    /// </summary>
    public interface IGetFavoriteByTarget
    {
        /// <summary>
        /// Retrieves a favorite record by patient and target (doctor or facility), including soft-deleted.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="doctorId">Optional doctor identifier.</param>
        /// <param name="facilityId">Optional facility identifier.</param>
        /// <returns>Matching favorite entity including soft-deleted; otherwise null.</returns>
        Task<Favorite?> Execute(Guid patientId, Guid? doctorId, Guid? facilityId);
    }
}
