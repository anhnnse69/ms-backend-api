using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllFacilities
{
    public interface IGetAllFacilities
    {
        IQueryable<Facility> Execute();
    }
}