using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.UpdateMedicalRecord
{
    /// <summary>
    /// Repository implementation used to update medical record
    /// </summary>
    public class UpdateMedicalRecordImpl :
        RepositoryBase<MedicalRecord, Guid, AppDbContext>,
        IUpdateMedicalRecord
    {
        /// <summary>
        /// Constructor for UpdateMedicalRecord repository
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="unitOfWork">Unit of work</param>
        public UpdateMedicalRecordImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Execute command to update medical record
        /// </summary>
        /// <param name="medicalRecord">Medical record entity</param>
        public async Task Execute(MedicalRecord medicalRecord)
        {
            await UpdateAsync(medicalRecord);
            await SaveChangesAsync();
        }
    }
}
