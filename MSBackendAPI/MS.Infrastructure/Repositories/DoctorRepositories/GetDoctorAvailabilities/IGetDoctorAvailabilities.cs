using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    public interface IGetDoctorAvailabilities
    {
        Task<IEnumerable<DoctorAvailability>> Execute(Guid doctorId);
    }
}
