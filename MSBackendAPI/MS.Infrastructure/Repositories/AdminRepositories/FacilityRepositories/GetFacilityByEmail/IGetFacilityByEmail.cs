using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByEmail
{
    /// <summary>
    /// Defines the contract for retrieving facility by email.
    /// </summary>
    public interface IGetFacilityByEmail
    {
        Task<Facility> Execute(string email);
    }
}