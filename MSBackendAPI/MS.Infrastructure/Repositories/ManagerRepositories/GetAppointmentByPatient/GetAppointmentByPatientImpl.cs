using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByPatient
{
    /// <summary>
    /// Repository implementation for retrieving appointment by patient ID and appointment ID
    /// </summary>
    public class GetAppointmentByPatientImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentByPatient
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the GetAppointmentByPatientImpl class.
        /// </summary>
        /// <param name="context">The database context used to access appointment data.</param>
        public GetAppointmentByPatientImpl(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get an appointment by patient ID and appointment ID with related data
        /// </summary>
        /// <param name="patientId">The patient ID</param>
        /// <param name="appointmentId">The appointment ID</param>
        /// <returns>The appointment entity or null if not found</returns>
        public async Task<Appointment> Execute(Guid patientId, Guid appointmentId)
        {
            return await FindByCondition(
                    a => a.PatientId == patientId
                         && a.Id == appointmentId
                         && !a.IsDeleted,
                    trackChanges: false)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Facility)
                .Include(a => a.Specialty)
                .FirstOrDefaultAsync();
        }
    }
}
