using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientAppointment
{
    /// <summary>
    /// Provides an implementation of the IGetAppointmentsByPatientId interface
    /// </summary>
    public class GetAppointmentsByPatientIdImpl : RepositoryQueryBase<Appointment, Guid, AppDbContext>, IGetAppointmentsByPatientId
    {
        /// <summary>
        /// Initializes a new instance of the GetAppointmentsByPatientIdImpl
        /// </summary>
        /// <param name="context">The database context</param>
        public GetAppointmentsByPatientIdImpl(AppDbContext context) : base(context)
        { }

        /// <summary>
        /// Process logic for retrieving appointments data by patient ID
        /// </summary>
        /// <param name="patientId">The ID of the patient</param>
        /// <returns>A list of appointment entities</returns>
        public async Task<List<Appointment>> Execute(Guid patientId)
        {
            return await FindByCondition(
                    a => a.PatientId == patientId, // Filter by PatientId
                    trackChanges: false,
                    a => a.Doctor,                 // Include Doctor for UI mapping
                    a => a.Facility,               // Include Facility for UI mapping
                    a => a.Specialty               // Include Specialty for UI mapping
                )
                .OrderByDescending(a => a.AppointmentTime) // Sort from newest to oldest (past & current)
                .ToListAsync();
        }
    }
}
