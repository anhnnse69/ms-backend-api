using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    public interface IGetDoctorAvailabilities
    {
        Task<IEnumerable<DoctorAvailability>> Execute(Guid doctorId);
    }
}
