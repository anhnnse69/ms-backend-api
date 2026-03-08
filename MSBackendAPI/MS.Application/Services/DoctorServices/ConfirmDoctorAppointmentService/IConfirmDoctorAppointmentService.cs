using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.ConfirmDoctorAppointmentService
{
    /// <summary>
    /// Interface for confirm doctor appointment service
    /// </summary>
    public interface IConfirmDoctorAppointmentService
    {
        /// <summary>
        /// Process confirm appointment request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Confirm appointment response</returns>
        Task<ApiResponse<ConfirmDoctorAppointmentResponse>> Process(Guid userId, Guid appointmentId);
    }
}
