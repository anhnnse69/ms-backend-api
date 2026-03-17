using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.UpdateAppointment
{
    /// <summary>
    /// Interface for Reschedule Appointment Service
    /// </summary>
    public interface IRescheduleAppointmentService
    {
        /// <summary>
        /// Process reschedule appointment request
        /// </summary>
        /// <param name="request">The request payload containing AppointmentId</param>
        /// <returns>API Response with mapped data</returns>
        Task<ApiResponse<RescheduleAppointmentResponse>> Process(RescheduleAppointmentRequest request);
    }
}