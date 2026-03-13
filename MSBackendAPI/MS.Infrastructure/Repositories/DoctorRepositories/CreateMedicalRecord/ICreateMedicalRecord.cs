using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.CreateMedicalRecord
{
    /// <summary>
    /// Repository for creating medical record
    /// </summary>
    public interface ICreateMedicalRecord
    {
        Task Execute(MedicalRecord medicalRecord);
    }
}
