using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId
{
    /// <summary>
    /// Repository implementation for retrieving a patient by their linked user identifier.
    /// </summary>
    public class GetPatientByUserIdImpl : RepositoryQueryBase<Patient, Guid, AppDbContext>, IGetPatientByUserIdRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientByUserIdImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetPatientByUserIdImpl(AppDbContext context) : base(context) { }

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
