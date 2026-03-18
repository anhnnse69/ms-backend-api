using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.SubmitReviewService;
using MS.Domain.Entities;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller responsible for handling patient review and rating submission requests.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/reviews")]
    [Authorize(Roles = "Patient")]
    public class SubmitPatientReviewController : ControllerBase
    {
        private readonly ISubmitReviewService _submitReviewService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitPatientReviewController"/> class.
        /// </summary>
        /// <param name="submitReviewService">Service responsible for review submission logic.</param>
        public SubmitPatientReviewController(ISubmitReviewService submitReviewService)
        {
            _submitReviewService = submitReviewService;
        }

        /// <summary>
        /// Submits a review and rating for a completed appointment by the authenticated patient.
        /// </summary>
        /// <param name="request">The request containing the appointment identifier, rating, and optional comment.</param>
        /// <returns>
        /// Returns the created review identifier and appointment identifier upon success.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] SubmitReviewRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value) || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }
            var result = await _submitReviewService.Process(request, userId);
            return Ok(result);
        }
    }
}
