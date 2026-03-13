using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId
{
    /// <summary>
    /// Repository implementation used to retrieve medical record by appointment identifier
    /// </summary>
    public class GetMedicalRecordByAppointmentIdImpl
        : RepositoryQueryBase<MedicalRecord, Guid, AppDbContext>,
          IGetMedicalRecordByAppointmentId
    {
        /// <summary>
        /// Constructor for GetMedicalRecordByAppointmentId repository
        /// </summary>
        /// <param name="context">Application database context</param>
        public GetMedicalRecordByAppointmentIdImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve medical record associated with an appointment
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Medical record entity</returns>
        public async Task<MedicalRecord?> Execute(Guid appointmentId)
        {
            return await FindByCondition(
                    x => x.AppointmentId == appointmentId,
                    false)
                .FirstOrDefaultAsync();
        }
    }
}