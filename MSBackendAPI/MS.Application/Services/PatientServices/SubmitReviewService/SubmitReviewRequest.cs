using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.SubmitReviewService
{
    /// <summary>
    /// Represents the request payload for submitting a review and rating after a completed appointment.
    /// </summary>
    public class SubmitReviewRequest
    {
        /// <summary>The unique identifier of the completed appointment being reviewed.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }
        /// <summary>The rating score given by the patient, between 1 and 5 stars.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [Range(1, 5, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int Rating { get; set; }
        /// <summary>Optional comment or feedback provided by the patient.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string? Comment { get; set; }
    }
}
