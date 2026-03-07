using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail
{
    public interface IGetUserByEmail
    {
        Task<User> Execute(string emailAddress);
    }
}
