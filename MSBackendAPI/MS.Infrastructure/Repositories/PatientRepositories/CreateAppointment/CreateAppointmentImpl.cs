using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreateAppointment
{
    /// <summary>
    /// Repository implementation for persisting a new appointment to the database.
    /// </summary>
    public class CreateAppointmentImpl : RepositoryBase<Appointment, Guid, AppDbContext>, ICreateAppointmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppointmentImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public CreateAppointmentImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Persists the specified appointment entity to the database.
        /// </summary>
        /// <param name="appointment">The appointment entity to persist.</param>
        /// <returns>The persisted appointment entity.</returns>
        public async Task<Appointment> Execute(Appointment appointment)
        {
            await CreateAsync(appointment);
            await SaveChangesAsync();
            return appointment;
        }
    }
}
