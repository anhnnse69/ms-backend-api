using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateAppointment
{
    /// <summary>
    /// Provides an implementation of the IUpdatePatientAppointment interface
    /// </summary>
    public class UpdatePatientAppointmentImpl : RepositoryBase<Appointment, Guid, AppDbContext>, IUpdatePatientAppointment
    {
        /// <summary>
        /// Initializes a new instance of the UpdatePatientAppointmentImpl class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="unitOfWork">The unit of work</param>
        public UpdatePatientAppointmentImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        { }

        /// <summary>
        /// Execute logic to update appointment
        /// </summary>
        /// <param name="appointment">The appointment entity to update</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public async Task Execute(Appointment appointment)
        {
            await UpdateAsync(appointment);
            await SaveChangesAsync();
        }
    }
}

