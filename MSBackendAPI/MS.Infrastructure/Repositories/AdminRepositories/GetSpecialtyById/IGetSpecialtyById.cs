using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetSpecialtyById
{
    public interface IGetSpecialtyById
    {
        Task<Specialty?> Execute(Guid id);
    }
}