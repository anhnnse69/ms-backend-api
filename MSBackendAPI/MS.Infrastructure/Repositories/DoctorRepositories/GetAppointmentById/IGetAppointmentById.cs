using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById
{
    public interface IGetAppointmentById
    {
        Task<Appointment> Execute(Guid appointmentId);
    }
}
