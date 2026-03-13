using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService
{
    /// <summary>
    /// Interface for update doctor medical record service
    /// </summary>
    public interface IUpdateDoctorMedicalRecordService
    {
        /// <summary>
        /// Process update medical record request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Update medical record request</param>
        /// <returns>Update medical record response</returns>
        Task<ApiResponse<UpdateDoctorMedicalRecordResponse>> Process(Guid userId, Guid appointmentId, UpdateDoctorMedicalRecordRequest request);
    }
}
