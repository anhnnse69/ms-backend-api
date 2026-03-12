using MS.Domain.Entities;


namespace MS.Infrastructure.Repositories.PatientRepositories.CreateUser
{
    public interface ICreateUser
    {
        Task<User> Execute(User user);
    }
}
