using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.CreateMedicalRecordService
{
    /// <summary>
    /// Interface for create medical record service
    /// </summary>
    public interface ICreateMedicalRecordService
    {
        /// <summary>
        /// Process create medical record request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Medical record request</param>
        /// <returns>Medical record response</returns>
        Task<ApiResponse<CreateMedicalRecordResponse>> Process(Guid userId, Guid appointmentId, CreateMedicalRecordRequest request);
    }
}
