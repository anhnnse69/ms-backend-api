using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers
{
    public interface IGetAllUsers
    {
        IQueryable<User> Execute();
    }
}
