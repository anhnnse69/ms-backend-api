using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByPhone
{
    public interface IGetFacilityByPhone
    {
        Task<Facility> Execute(string phone);
    }
}