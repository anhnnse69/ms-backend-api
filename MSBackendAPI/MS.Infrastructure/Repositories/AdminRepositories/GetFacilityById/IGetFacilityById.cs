using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById
{
    public interface IGetFacilityById
    {
        Task<Facility?> Execute(Guid id);
    }
}
