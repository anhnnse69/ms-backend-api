using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserIdForReview
{
    /// <summary>
    /// Repository interface for retrieving a patient by their linked user identifier for review submission.
    /// </summary>
    public interface IGetPatientByUserIdForReview
    {
        /// <summary>
        /// Retrieves the patient entity associated with the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier linked to the patient.</param>
        /// <returns>Patient entity if found; otherwise null.</returns>
        Task<Patient?> Execute(Guid userId);
    }
}
