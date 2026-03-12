using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreateNewAccount
{
    /// <summary>
    /// Contract for creating a new user (and associated roles like Patient/Doctor).
    /// </summary>
    public interface ICreateAccount
    {
        Task<User> Execute(User user);
    }
}
