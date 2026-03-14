using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetPendingAppointmentPatient
{
    /// <summary>
    /// Repository implementation for retrieving pending patient appointments
    /// </summary>
    public class GetPendingAppointmentPatientImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetPendingAppointmentPatient
    {
        /// <summary>
        /// Initializes a new instance of the repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetPendingAppointmentPatientImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve a paginated list of pending appointments
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// A tuple containing:
        /// - List of appointment entities
        /// - Total number of records matching the query
        /// </returns>
        public async Task<(List<Appointment>, int)> Execute(int page, int size)
        {
            // Build query to retrieve pending appointments
            var query = FindByCondition(
                    a => a.Status == (int)AppointmentStatus.PendingConfirmation && !a.IsDeleted,
                    false)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Facility)
                .Include(a => a.Specialty)
                .OrderBy(a => a.AppointmentTime);
            // Get total number of matching records
            var total = await query.CountAsync();
            // Retrieve paginated data
            var appointments = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            // Return result tuple
            return (appointments, total);
        }
    }
}