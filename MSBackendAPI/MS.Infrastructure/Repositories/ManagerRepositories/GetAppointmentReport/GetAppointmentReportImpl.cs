using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentReport
{
    /// <summary>
    /// Repository implementation for retrieving appointment data used in report generation.
    /// </summary>
    public class GetAppointmentReportImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAppointmentReportImpl"/> class.
        /// </summary>
        /// <param name="context">
        /// Database context used to access appointment data.
        /// </param>
        public GetAppointmentReportImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves appointment query used for generating reports.
        /// Only non-deleted appointments are included.
        /// </summary>
        /// <returns>
        /// IQueryable collection of appointment entities for further filtering.
        /// </returns>
        public IQueryable<Appointment> Execute()
        {
            return FindByCondition(
                a => !a.IsDeleted,
                false);
        }
    }
}