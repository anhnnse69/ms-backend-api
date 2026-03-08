using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById
{
    /// <summary>
    /// Repository implementation used to retrieve appointment by identifier
    /// </summary>
    public class GetAppointmentByIdImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentById
    {
        /// <summary>
        /// Constructor for GetAppointmentById repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetAppointmentByIdImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve appointment entity by identifier
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Appointment entity</returns>
        public async Task<Appointment> Execute(Guid appointmentId)
        {
            return await FindByCondition(x => x.Id == appointmentId, false)
                // Include patient information related to appointment
                .Include(x => x.Patient)
                // Include facility where the appointment takes place
                .Include(x => x.Facility)
                .FirstOrDefaultAsync();
        }
    }
}