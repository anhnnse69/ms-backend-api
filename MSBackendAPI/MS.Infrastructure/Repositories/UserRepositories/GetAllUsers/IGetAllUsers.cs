using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.UserRepositories.GetAllUsers
{
    public interface IGetAllUsers
    {
        Task<List<User>> Execute();
    }
}
