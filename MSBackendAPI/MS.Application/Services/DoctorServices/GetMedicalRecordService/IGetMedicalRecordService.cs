using MS.Application.Common.Response;
using MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService;

namespace MS.Application.Services.DoctorServices.GetMedicalRecordService
{
    /// <summary>
    /// Interface for retrieving medical record by appointment
    /// </summary>
    public interface IGetMedicalRecordService
    {
        /// <summary>
        /// Process get medical record request
        /// </summary>
        /// <param name="userId">User identifier from JWT</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Medical record response</returns>
        Task<ApiResponse<UpdateDoctorMedicalRecordResponse>> Process(Guid userId, Guid appointmentId);
    }
}