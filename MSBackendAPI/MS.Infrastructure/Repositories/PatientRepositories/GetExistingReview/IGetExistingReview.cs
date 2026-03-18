using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetExistingReview
{
    /// <summary>
    /// Repository interface for checking whether a review already exists for a given appointment.
    /// </summary>
    public interface IGetExistingReview
    {
        /// <summary>
        /// Retrieves an existing review entity for the specified appointment identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment to check.</param>
        /// <returns>Existing Review entity if found; otherwise null.</returns>
        Task<Review?> Execute(Guid appointmentId);
    }
}
