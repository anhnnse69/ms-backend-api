using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.RejectDoctorAppointmentService
{
    /// <summary>
    /// Interface for reject doctor appointment service
    /// </summary>
    public interface IRejectDoctorAppointmentService
    {
        /// <summary>
        /// Process reject appointment request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="request">Reject appointment request model</param>
        /// <returns>Reject appointment response</returns>
        Task<ApiResponse<RejectDoctorAppointmentResponse>> Process(Guid userId, RejectDoctorAppointmentRequest request);
    }
}
