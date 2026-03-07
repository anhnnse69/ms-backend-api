using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId
{
    public interface IGetDoctorByUserId
    {
        Task<Doctor> Execute(Guid userId);
    }
}
