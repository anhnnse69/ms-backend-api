using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateUser
{
    public interface IUpdateUser
    {
        Task Execute(User user);
    }
}
