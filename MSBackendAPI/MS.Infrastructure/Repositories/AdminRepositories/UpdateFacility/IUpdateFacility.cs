using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility
{
    public interface IUpdateFacility
    {
        Task Execute(Facility facility);
    }
}