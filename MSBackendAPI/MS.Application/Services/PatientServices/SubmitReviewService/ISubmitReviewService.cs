using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.SubmitReviewService
{
    /// <summary>
    /// Defines the contract for the submit review service.
    /// </summary>
    public interface ISubmitReviewService
    {
        /// <summary>
        /// Processes the request to submit a review and rating for a completed appointment.
        /// </summary>
        /// <param name="request">The request containing review details including rating and comment.</param>
        /// <param name="patientUserId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created review identifier and appointment identifier.
        /// </returns>
        Task<ApiResponse<SubmitReviewResponse>> Process(SubmitReviewRequest request, Guid patientUserId);
    }
}
