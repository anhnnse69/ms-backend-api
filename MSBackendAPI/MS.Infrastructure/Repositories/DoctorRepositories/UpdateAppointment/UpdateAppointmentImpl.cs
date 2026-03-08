using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment
{
    /// <summary>
    /// Repository implementation used to update appointment entity
    /// </summary>
    public class UpdateAppointmentImpl : RepositoryBase<Appointment, Guid, AppDbContext>, IUpdateAppointment
    {
        /// <summary>
        /// Constructor for UpdateAppointment repository
        /// </summary>
        /// <param name="context">Application database context</param>
        /// <param name="unitOfWork">Unit of work instance</param>
        public UpdateAppointmentImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Execute update appointment entity
        /// </summary>
        /// <param name="appointment">Appointment entity to be updated</param>
        /// <returns>Task representing asynchronous operation</returns>
        public async Task Execute(Appointment appointment)
        {
            await UpdateAsync(appointment);
            await SaveChangesAsync();
        }
    }
}