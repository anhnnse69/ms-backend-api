using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByName
{
    public interface IGetFacilityByName
    {
        Task<Facility> Execute(string name);
    }
}