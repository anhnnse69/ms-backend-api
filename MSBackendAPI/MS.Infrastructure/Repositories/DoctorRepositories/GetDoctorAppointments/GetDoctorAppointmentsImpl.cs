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
    public class GetDoctorAppointmentsImpl
       : RepositoryQueryBase<Appointment, Guid, AppDbContext>,
         IGetDoctorAppointments
    {
        public GetDoctorAppointmentsImpl(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> Execute(Guid doctorId)
        {
            return await FindByCondition(x => x.DoctorId == doctorId, false)
                .Include(x => x.Patient)
                .Include(x => x.Facility)
                .ToListAsync();
        }
    }
}
