using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers
{
    public interface IGetAllUsers
    {
        Task<List<User>> Execute();
    }
}
