using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.UpdateMedicalRecord
{
    /// <summary>
    /// Repository used to update medical record
    /// </summary>
    public interface IUpdateMedicalRecord
    {
        /// <summary>
        /// Execute command to update medical record
        /// </summary>
        /// <param name="medicalRecord">Medical record entity</param>
        Task Execute(MedicalRecord medicalRecord);
    }
}
