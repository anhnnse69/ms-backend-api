using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments
{
    /// <summary>
    /// Repository implementation used to retrieve doctor appointment list
    /// </summary>
    public class GetDoctorAppointmentsImpl
        : RepositoryQueryBase<Appointment, Guid, AppDbContext>,
          IGetDoctorAppointments
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetDoctorAppointmentsImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve appointment list of a doctor
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>IQueryable appointment query</returns>
        public IQueryable<Appointment> Execute(Guid doctorId)
        {
            return FindByCondition(x => x.DoctorId == doctorId, false)
                .Include(x => x.Patient)
                .Include(x => x.Facility);
        }
    }
}