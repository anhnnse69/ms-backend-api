using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments
{
    public interface IGetDoctorAppointments
    {
        IQueryable<Appointment> Execute(Guid doctorId);
    }
}
