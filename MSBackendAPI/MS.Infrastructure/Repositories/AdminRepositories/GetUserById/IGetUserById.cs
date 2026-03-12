using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetUserById
{
    public interface IGetUserById
    {
        Task<User> Execute(Guid id);
    }
}
