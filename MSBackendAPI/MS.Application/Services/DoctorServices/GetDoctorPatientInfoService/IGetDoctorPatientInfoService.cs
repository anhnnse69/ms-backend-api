using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.GetDoctorPatientInfoService
{
    /// <summary>
    /// Interface for retrieving patient information for doctor
    /// </summary>
    public interface IGetDoctorPatientInfoService
    {
        /// <summary>
        /// Process request to retrieve patient information
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Patient information response</returns>
        Task<ApiResponse<GetDoctorPatientInfoResponse>> Process(
            Guid userId,
            Guid appointmentId);
    }
}
