namespace MS.Application.Services.PatientServices.SubmitReviewService
{
    /// <summary>
    /// Represents the response returned after successfully submitting a review.
    /// </summary>
    public class SubmitReviewResponse
    {
        /// <summary>The unique identifier of the newly created review.</summary>
        public Guid Id { get; set; }
        /// <summary>The unique identifier of the appointment that was reviewed.</summary>
        public Guid AppointmentId { get; set; }
    }
}
