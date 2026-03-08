using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    public interface IGetDoctorAvailabilities
    {
        IQueryable<DoctorAvailability> Execute(Guid doctorId);
    }
}
