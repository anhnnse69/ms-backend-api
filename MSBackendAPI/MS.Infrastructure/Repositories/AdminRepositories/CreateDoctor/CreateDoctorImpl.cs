using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.CreateDoctor
{
    /// <summary>
    /// Provides an implementation of the ICreateDoctor interface
    /// for inserting a new doctor into the database.
    /// </summary>
    public class CreateDoctorImpl : RepositoryBase<Doctor, Guid, AppDbContext>, ICreateDoctor
    {
        /// <summary>
        /// Initializes a new instance of the CreateDoctorImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access doctor data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work responsible for managing database transactions.
        /// </param>
        public CreateDoctorImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) { }

        /// <summary>
        /// Inserts a new doctor record into the database.
        /// </summary>
        /// <param name="doctor">
        /// The <see cref="Doctor"/> entity to be created.
        /// </param>
        /// <returns>
        /// The created <see cref="Doctor"/> entity.
        /// </returns>
        public async Task<Doctor> Execute(Doctor doctor)
        {
            await CreateAsync(doctor);
            await SaveChangesAsync();
            return doctor;
        }
    }
}
