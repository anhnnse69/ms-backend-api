using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Interface for update doctor appointment status service
    /// </summary>
    public interface IUpdateDoctorAppointmentStatusService
    {
        /// <summary>
        /// Process update appointment status request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="request">Update appointment status request model</param>
        /// <returns>Update appointment status response</returns>
        Task<ApiResponse<UpdateDoctorAppointmentStatusResponse>> Process(Guid userId, UpdateDoctorAppointmentStatusRequest request);
    }
}
