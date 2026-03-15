using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByPhone
{
    public interface IGetFacilityByPhone
    {
        Task<Facility> Execute(string phone);
    }
}