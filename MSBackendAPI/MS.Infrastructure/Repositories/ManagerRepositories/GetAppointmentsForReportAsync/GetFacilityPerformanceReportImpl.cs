using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentsForReportAsync
{
    /// <summary>
    /// Repository implementation for retrieving facility performance report data.
    /// </summary>
    public class GetFacilityPerformanceReportImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetFacilityPerformanceReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityPerformanceReportImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetFacilityPerformanceReportImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a list of appointments matching the specified criteria.
        /// </summary>
        /// <param name="facilityId">The facility identifier.</param>
        /// <param name="startDate">Optional start date filter (inclusive).</param>
        /// <param name="endDate">Optional end date filter (inclusive).</param>
        /// <param name="doctorId">Optional doctor identifier filter.</param>
        /// <returns>List of appointments with related doctor data.</returns>
        public async Task<List<Appointment>> Execute(Guid facilityId, DateTime? startDate, DateTime? endDate, Guid? doctorId)
        {
            IQueryable<Appointment> query = FindByCondition(a => a.FacilityId == facilityId, false)
                .Include(a => a.Doctor);
            if (doctorId.HasValue)
            {
                query = query.Where(a => a.DoctorId == doctorId.Value);
            }
            if (startDate.HasValue)
            {
                var startDateOffset = new DateTimeOffset(DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc));
                query = query.Where(a => a.AppointmentTime >= startDateOffset);
            }
            if (endDate.HasValue)
            {
                var endDateOffset = new DateTimeOffset(DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc));
                query = query.Where(a => a.AppointmentTime <= endDateOffset);
            }
            return await query.ToListAsync();
        }
    }
}