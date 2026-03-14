using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetSpecialtiesByFacilityId
{
    /// <summary>
    /// Interface for retrieving specialties by facility ID
    /// </summary>
    public interface IGetSpecialtiesByFacilityId
    {
        Task<List<FacilitySpecialty>> Execute(Guid facilityId);
    }
}
