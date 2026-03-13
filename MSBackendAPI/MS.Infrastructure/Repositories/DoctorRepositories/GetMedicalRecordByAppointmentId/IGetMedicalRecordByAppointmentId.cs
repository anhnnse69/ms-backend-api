using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId
{
    /// <summary>
    /// Repository used to retrieve medical record by appointment id
    /// </summary>
    public interface IGetMedicalRecordByAppointmentId
    {
        Task<MedicalRecord?> Execute(Guid appointmentId);
    }
}
