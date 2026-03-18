using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetExistingReview
{
    /// <summary>
    /// Repository implementation for checking whether a review already exists for a given appointment.
    /// </summary>
    public class GetExistingReviewImpl : RepositoryQueryBase<Review, Guid, AppDbContext>, IGetExistingReview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExistingReviewImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetExistingReviewImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves an existing review entity for the specified appointment identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment to check.</param>
        /// <returns>Existing Review entity if found; otherwise null.</returns>
        public async Task<Review?> Execute(Guid appointmentId)
        {
            return await FindByCondition(r => r.AppointmentId == appointmentId, false)
                .FirstOrDefaultAsync();
        }
    }
}
