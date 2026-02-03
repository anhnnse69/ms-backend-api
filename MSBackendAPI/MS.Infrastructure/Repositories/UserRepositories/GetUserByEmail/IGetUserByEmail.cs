using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.UserRepositories.GetUserByEmail
{
    public interface IGetUserByEmail
    {
        Task<User> Execute(string emailAddress);
    }
}
