using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments
{
    public interface IGetDoctorAppointments
    {
        Task<IEnumerable<Appointment>> Execute(Guid doctorId);
    }
}
