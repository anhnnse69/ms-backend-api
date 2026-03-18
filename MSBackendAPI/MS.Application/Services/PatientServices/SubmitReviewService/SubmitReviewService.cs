using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.PatientRepositories.CreateReview;
using MS.Infrastructure.Repositories.PatientRepositories.GetAppointmentForReview;
using MS.Infrastructure.Repositories.PatientRepositories.GetExistingReview;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserIdForReview;

namespace MS.Application.Services.PatientServices.SubmitReviewService
{
    /// <summary>
    /// Service responsible for handling the submit review and rating business logic.
    /// </summary>
    public class SubmitReviewService : ISubmitReviewService
    {
        private readonly IGetPatientByUserIdForReview _getPatientRepo;
        private readonly IGetAppointmentForReview _getAppointmentRepo;
        private readonly IGetExistingReview _getExistingReviewRepo;
        private readonly ICreateReview _createReviewRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitReviewService"/> class.
        /// </summary>
        /// <param name="getPatientRepo">Repository responsible for patient data access.</param>
        /// <param name="getAppointmentRepo">Repository responsible for appointment data access.</param>
        /// <param name="getExistingReviewRepo">Repository responsible for checking existing review data.</param>
        /// <param name="createReviewRepo">Repository responsible for review creation.</param>
        public SubmitReviewService(
            IGetPatientByUserIdForReview getPatientRepo,
            IGetAppointmentForReview getAppointmentRepo,
            IGetExistingReview getExistingReviewRepo,
            ICreateReview createReviewRepo)
        {
            _getPatientRepo = getPatientRepo;
            _getAppointmentRepo = getAppointmentRepo;
            _getExistingReviewRepo = getExistingReviewRepo;
            _createReviewRepo = createReviewRepo;
        }

        /// <summary>
        /// Processes the request to submit a review and rating for a completed appointment.
        /// </summary>
        /// <param name="request">The request containing review details including rating and comment.</param>
        /// <param name="patientUserId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created review identifier and appointment identifier.
        /// </returns>
        public async Task<ApiResponse<SubmitReviewResponse>> Process(SubmitReviewRequest request, Guid patientUserId)
        {
            // 1. Initialize validation flags
            bool isPatientValid = true;
            bool isAppointmentValid = true;
            bool isOwnerValid = true;
            bool isStatusValid = true;
            bool isNotDuplicateValid = true;
            // 2. Retrieve data from repositories
            var patient = await RetrievePatient(patientUserId);
            var appointment = await RetrieveAppointment(request.AppointmentId);
            var existingReview = await RetrieveExistingReview(request.AppointmentId);
            // 3. Validate retrieved data
            ValidatePatient(patient, ref isPatientValid);
            ValidateAppointment(appointment, ref isAppointmentValid);
            ValidateOwnership(appointment, patient, ref isOwnerValid);
            ValidateAppointmentStatus(appointment, ref isStatusValid);
            ValidateDuplicate(existingReview, ref isNotDuplicateValid);
            // 4. Execute side effects
            var review = await ExecuteCreate(request, patient, appointment, isPatientValid, isAppointmentValid, isOwnerValid, isStatusValid, isNotDuplicateValid);
            // 5. Map to response
            var response = MapToResponse(review);
            // 6. Return API response
            return CreateResponse(response, isPatientValid, isAppointmentValid, isOwnerValid, isStatusValid, isNotDuplicateValid);
        }

        /// <summary>
        /// Retrieves the patient entity by the authenticated user identifier.
        /// </summary>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>Patient entity if found; otherwise null.</returns>
        private async Task<Patient?> RetrievePatient(Guid userId)
        {
            return await _getPatientRepo.Execute(userId);
        }

        /// <summary>
        /// Retrieves the appointment entity by its identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>Appointment entity if found; otherwise null.</returns>
        private async Task<Appointment?> RetrieveAppointment(Guid appointmentId)
        {
            return await _getAppointmentRepo.Execute(appointmentId);
        }

        /// <summary>
        /// Retrieves an existing review for the specified appointment identifier.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of the appointment.</param>
        /// <returns>Existing Review entity if found; otherwise null.</returns>
        private async Task<Review?> RetrieveExistingReview(Guid appointmentId)
        {
            return await _getExistingReviewRepo.Execute(appointmentId);
        }

        /// <summary>
        /// Validates whether the patient entity exists.
        /// </summary>
        /// <param name="patient">The patient entity to validate.</param>
        /// <param name="isPatientValid">Flag; set to false if patient is null.</param>
        private void ValidatePatient(Patient? patient, ref bool isPatientValid)
        {
            if (patient == null)
            {
                isPatientValid = false;
            }
        }

        /// <summary>
        /// Validates whether the appointment entity exists.
        /// </summary>
        /// <param name="appointment">The appointment entity to validate.</param>
        /// <param name="isAppointmentValid">Flag; set to false if appointment is null.</param>
        private void ValidateAppointment(Appointment? appointment, ref bool isAppointmentValid)
        {
            if (appointment == null)
            {
                isAppointmentValid = false;
            }
        }

        /// <summary>
        /// Validates whether the appointment belongs to the requesting patient.
        /// </summary>
        /// <param name="appointment">The appointment entity to validate ownership of.</param>
        /// <param name="patient">The patient entity to check ownership against.</param>
        /// <param name="isOwnerValid">Flag; set to false if the appointment does not belong to the patient.</param>
        private void ValidateOwnership(Appointment? appointment, Patient? patient, ref bool isOwnerValid)
        {
            if (appointment != null && patient != null && appointment.PatientId != patient.Id)
            {
                isOwnerValid = false;
            }
        }

        /// <summary>
        /// Validates whether the appointment status is Completed.
        /// </summary>
        /// <param name="appointment">The appointment entity to validate the status of.</param>
        /// <param name="isStatusValid">Flag; set to false if the appointment status is not Completed.</param>
        private void ValidateAppointmentStatus(Appointment? appointment, ref bool isStatusValid)
        {
            if (appointment != null && appointment.Status != AppointmentStatus.Completed)
            {
                isStatusValid = false;
            }
        }

        /// <summary>
        /// Validates whether a review for the appointment does not already exist.
        /// </summary>
        /// <param name="existingReview">The existing review entity to check for duplication.</param>
        /// <param name="isNotDuplicateValid">Flag; set to false if a review already exists for the appointment.</param>
        private void ValidateDuplicate(Review? existingReview, ref bool isNotDuplicateValid)
        {
            if (existingReview != null)
            {
                isNotDuplicateValid = false;
            }
        }

        /// <summary>
        /// Executes the review creation when all validation flags are valid.
        /// </summary>
        /// <param name="request">The submit review request data.</param>
        /// <param name="patient">The resolved patient entity.</param>
        /// <param name="appointment">The resolved appointment entity.</param>
        /// <param name="isPatientValid">Flag indicating patient is valid.</param>
        /// <param name="isAppointmentValid">Flag indicating appointment is valid.</param>
        /// <param name="isOwnerValid">Flag indicating ownership is valid.</param>
        /// <param name="isStatusValid">Flag indicating appointment status is valid.</param>
        /// <param name="isNotDuplicateValid">Flag indicating no duplicate review exists.</param>
        /// <returns>The created review entity; otherwise null.</returns>
        private async Task<Review?> ExecuteCreate(
            SubmitReviewRequest request,
            Patient? patient,
            Appointment? appointment,
            bool isPatientValid,
            bool isAppointmentValid,
            bool isOwnerValid,
            bool isStatusValid,
            bool isNotDuplicateValid)
        {
            if (!isPatientValid || !isAppointmentValid || !isOwnerValid || !isStatusValid || !isNotDuplicateValid)
            {
                return null;
            }
            var review = new Review
            {
                PatientId = patient!.Id,
                AppointmentId = request.AppointmentId,
                DoctorId = appointment!.DoctorId,
                FacilityId = appointment.FacilityId,
                Rating = request.Rating,
                Comment = request.Comment,
                IsVisible = true,
                CreateBy = patient.Id.ToString()
            };
            return await _createReviewRepo.Execute(review);
        }

        /// <summary>
        /// Maps the created review entity to <see cref="SubmitReviewResponse"/>.
        /// </summary>
        /// <param name="review">The created review entity to map.</param>
        /// <returns>Mapped <see cref="SubmitReviewResponse"/>; default instance if review is null.</returns>
        private SubmitReviewResponse MapToResponse(Review? review)
        {
            if (review == null)
            {
                return new SubmitReviewResponse();
            }
            return new SubmitReviewResponse
            {
                Id = review.Id,
                AppointmentId = review.AppointmentId
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isPatientValid">Flag indicating patient is valid.</param>
        /// <param name="isAppointmentValid">Flag indicating appointment is valid.</param>
        /// <param name="isOwnerValid">Flag indicating ownership is valid.</param>
        /// <param name="isStatusValid">Flag indicating appointment status is valid.</param>
        /// <param name="isNotDuplicateValid">Flag indicating no duplicate review exists.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<SubmitReviewResponse> CreateResponse(
            SubmitReviewResponse response,
            bool isPatientValid,
            bool isAppointmentValid,
            bool isOwnerValid,
            bool isStatusValid,
            bool isNotDuplicateValid)
        {
            if (!isPatientValid)
                return ApiResponse<SubmitReviewResponse>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            if (!isAppointmentValid)
                return ApiResponse<SubmitReviewResponse>.Fail(MessageCode.APP_MESSAGE_4012.ToString());
            if (!isOwnerValid)
                return ApiResponse<SubmitReviewResponse>.Fail(MessageCode.APP_MESSAGE_4014.ToString());
            if (!isStatusValid)
                return ApiResponse<SubmitReviewResponse>.Fail(MessageCode.APP_MESSAGE_4013.ToString());
            if (!isNotDuplicateValid)
                return ApiResponse<SubmitReviewResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            return ApiResponse<SubmitReviewResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
