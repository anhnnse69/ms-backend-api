using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    public class GetDoctorAvailabilitiesImpl
          : RepositoryQueryBase<DoctorAvailability, Guid, AppDbContext>,
            IGetDoctorAvailabilities
    {
        public GetDoctorAvailabilitiesImpl(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DoctorAvailability>> Execute(Guid doctorId)
        {
            return await FindByCondition(x => x.DoctorId == doctorId, false)
                .Include(x => x.Facility)
                .ToListAsync();
        }
    }
}
