using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateUser
{
    public interface IUpdateUser
    {
        Task Execute(User user);
    }
}
