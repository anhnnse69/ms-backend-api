using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateDoctor
{
    /// <summary>
    /// Provides an implementation for updating doctor data.
    /// </summary>
    public class UpdateDoctorImpl : RepositoryBase<Doctor, Guid, AppDbContext>, IUpdateDoctor
    {
        /// <summary>
        /// Initializes a new instance of the UpdateDoctorImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work instance for managing transactions.
        /// </param>
        public UpdateDoctorImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) 
        { 
        }

        /// <summary>
        /// Updates the specified doctor entity and persists changes to the database.
        /// </summary>
        /// <param name="doctor">
        /// The doctor entity containing updated information.
        /// </param>
        /// <returns>
        /// The updated <see cref="Doctor"/> entity after persistence.
        /// </returns>
        public async Task<Doctor> Execute(Doctor doctor)
        {
            // Mark entity as updated
            await UpdateAsync(doctor);
            // Save changes to database
            await SaveChangesAsync();
            return doctor;
        }
    }
}
