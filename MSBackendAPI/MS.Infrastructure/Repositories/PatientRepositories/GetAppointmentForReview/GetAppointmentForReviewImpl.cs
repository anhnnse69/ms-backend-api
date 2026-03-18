using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetAppointmentForReview
{
    /// <summary>
    /// Repository implementation for retrieving an appointment by its identifier for review submission.
    /// </summary>
    public class GetAppointmentForReviewImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentForReview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAppointmentForReviewImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetAppointmentForReviewImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the appointment entity matching the specified identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>Appointment entity if found; otherwise null.</returns>
        public async Task<Appointment?> Execute(Guid appointmentId)
        {
            return await FindByCondition(a => a.Id == appointmentId, false)
                .FirstOrDefaultAsync();
        }
    }
}
