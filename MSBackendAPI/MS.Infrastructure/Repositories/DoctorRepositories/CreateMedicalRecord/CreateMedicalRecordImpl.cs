using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.CreateMedicalRecord
{
    /// <summary>
    /// Repository implementation used to create medical record
    /// </summary>
    public class CreateMedicalRecordImpl :
        RepositoryBase<MedicalRecord, Guid, AppDbContext>,
        ICreateMedicalRecord
    {
        /// <summary>
        /// Constructor for CreateMedicalRecord repository
        /// </summary>
        /// <param name="context">Application database context</param>
        /// <param name="unitOfWork">Unit of work instance</param>
        public CreateMedicalRecordImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Execute command to create medical record
        /// </summary>
        /// <param name="medicalRecord">Medical record entity</param>
        public async Task Execute(MedicalRecord medicalRecord)
        {
            await CreateAsync(medicalRecord);
            await SaveChangesAsync();
        }
    }
}
