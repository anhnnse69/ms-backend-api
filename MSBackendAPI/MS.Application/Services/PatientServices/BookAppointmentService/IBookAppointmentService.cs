using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.BookAppointmentService
{
    /// <summary>
    /// Defines the contract for the book appointment service.
    /// </summary>
    public interface IBookAppointmentService
    {
        /// <summary>
        /// Processes the request to book a new medical appointment for a patient.
        /// </summary>
        /// <param name="request">The request containing appointment booking details.</param>
        /// <param name="patientUserId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created appointment id and status.
        /// </returns>
        Task<ApiResponse<BookAppointmentResponse>> Process(BookAppointmentRequest request, Guid patientUserId);
    }
}
