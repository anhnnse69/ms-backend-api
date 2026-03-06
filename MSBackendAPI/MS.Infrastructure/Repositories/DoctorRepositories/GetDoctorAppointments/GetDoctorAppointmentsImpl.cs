using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Constructor for GetDoctorAppointments repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetDoctorAppointmentsImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve appointment list of a doctor
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Collection of appointment entities</returns>
        public async Task<IEnumerable<Appointment>> Execute(Guid doctorId)
        {
            return await FindByCondition(x => x.DoctorId == doctorId, false)

                // Include patient information related to appointment
                .Include(x => x.Patient)
                // Include facility where the appointment takes place
                .Include(x => x.Facility)
                .ToListAsync();
        }
    }
}
