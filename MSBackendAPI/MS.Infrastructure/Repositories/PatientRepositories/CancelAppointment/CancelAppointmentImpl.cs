using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.CancelAppointment
{
    /// <summary>
    /// Provides an implementation for cancelling an appointment
    /// </summary>
    public class CancelAppointmentImpl : RepositoryBase<Appointment, Guid, AppDbContext>, ICancelAppointment
    {
        public CancelAppointmentImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        { }

        /// <summary>
        /// Updates the appointment entity and commits changes to the database
        /// </summary>
        /// <param name="appointment"></param>
        public async Task Execute(Appointment appointment)
        {
            await UpdateAsync(appointment);
            await SaveChangesAsync();
        }
    }
}
