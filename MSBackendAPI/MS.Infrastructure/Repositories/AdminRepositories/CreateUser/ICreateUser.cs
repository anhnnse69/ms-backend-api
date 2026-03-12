using MS.Domain.Entities;


namespace MS.Infrastructure.Repositories.AdminRepositories.CreateUser
{
    public interface ICreateUser
    {
        Task<User> Execute(User user);
    }
}
