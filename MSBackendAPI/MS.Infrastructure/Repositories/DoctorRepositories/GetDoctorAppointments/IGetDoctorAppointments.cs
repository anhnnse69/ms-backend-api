using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments
{
    public interface IGetDoctorAppointments
    {
        Task<IEnumerable<Appointment>> Execute(Guid doctorId);
    }
}
