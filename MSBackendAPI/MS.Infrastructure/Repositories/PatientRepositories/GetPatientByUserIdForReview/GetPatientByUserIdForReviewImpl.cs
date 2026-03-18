using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserIdForReview
{
    /// <summary>
    /// Repository implementation for retrieving a patient by their linked user identifier for review submission.
    /// </summary>
    public class GetPatientByUserIdForReviewImpl : RepositoryQueryBase<Patient, Guid, AppDbContext>, IGetPatientByUserIdForReview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientByUserIdForReviewImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetPatientByUserIdForReviewImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the patient entity associated with the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier linked to the patient.</param>
        /// <returns>Patient entity if found; otherwise null.</returns>
        public async Task<Patient?> Execute(Guid userId)
        {
            return await FindByCondition(p => p.UserId == userId, false)
                .FirstOrDefaultAsync();
        }
    }
}
