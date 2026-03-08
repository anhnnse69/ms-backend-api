using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment
{
    public interface IUpdateAppointment
    {
        Task Execute(Appointment appointment);
    }
}
