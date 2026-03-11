using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetUserById
{
    public interface IGetUserById
    {
        Task<User> Execute(Guid id);
    }
}
