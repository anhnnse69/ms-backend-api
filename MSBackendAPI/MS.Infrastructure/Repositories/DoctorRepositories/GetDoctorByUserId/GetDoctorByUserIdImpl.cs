using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId
{
    public class GetDoctorByUserIdImpl
    : RepositoryQueryBase<Doctor, Guid, AppDbContext>,
      IGetDoctorByUserId
    {
        public GetDoctorByUserIdImpl(AppDbContext context) : base(context)
        {
        }

        public async Task<Doctor> Execute(Guid userId)
        {
            return await FindByCondition(x => x.UserId == userId, false)
                .FirstOrDefaultAsync();
        }
    }
}
