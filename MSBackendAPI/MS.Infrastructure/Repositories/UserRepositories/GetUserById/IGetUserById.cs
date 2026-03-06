using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.UserRepositories.GetUserById
{
    public interface IGetUserById
    {
        Task<User> Execute(Guid id);
    }
}
