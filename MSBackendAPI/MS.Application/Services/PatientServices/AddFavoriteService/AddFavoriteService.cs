using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetDoctorForBooking;
using MS.Infrastructure.Repositories.PatientRepositories.GetFacilityForBooking;
using MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteByTarget;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId;
using MS.Infrastructure.Repositories.PatientRepositories.RestoreFavorite;
using MS.Infrastructure.Repositories.PatientRepositories.AddFavorite;

namespace MS.Application.Services.PatientServices.AddFavoriteService
{
    /// <summary>
    /// Handles the business logic for adding a doctor or facility to the patient's favorites.
    /// </summary>
    public class AddFavoriteService : IAddFavoriteService
    {
        private readonly IGetPatientByUserId _getPatientByUserId;
        private readonly IGetDoctorForBooking _getDoctorForBooking;
        private readonly IGetFacilityForBooking _getFacilityForBooking;
        private readonly IGetFavoriteByTarget _getFavoriteByTarget;
        private readonly IAddFavorite _addFavorite;
        private readonly IRestoreFavorite _restoreFavorite;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFavoriteService"/> class.
        /// </summary>
        /// <param name="getPatientByUserId">Repository responsible for retrieving patient by user identifier.</param>
        /// <param name="getDoctorForBooking">Repository responsible for verifying doctor existence.</param>
        /// <param name="getFacilityForBooking">Repository responsible for verifying facility existence.</param>
        /// <param name="getFavoriteByTarget">Repository responsible for checking existing favorite records.</param>
        /// <param name="addFavorite">Repository responsible for creating a new favorite record.</param>
        /// <param name="restoreFavorite">Repository responsible for restoring a soft-deleted favorite.</param>
        public AddFavoriteService(
            IGetPatientByUserId getPatientByUserId,
            IGetDoctorForBooking getDoctorForBooking,
            IGetFacilityForBooking getFacilityForBooking,
            IGetFavoriteByTarget getFavoriteByTarget,
            IAddFavorite addFavorite,
            IRestoreFavorite restoreFavorite)
        {
            _getPatientByUserId = getPatientByUserId;
            _getDoctorForBooking = getDoctorForBooking;
            _getFacilityForBooking = getFacilityForBooking;
            _getFavoriteByTarget = getFavoriteByTarget;
            _addFavorite = addFavorite;
            _restoreFavorite = restoreFavorite;
        }

        /// <summary>
        /// Processes the request to add a doctor or facility to the patient's favorites.
        /// </summary>
        /// <param name="request">The request containing the doctorId or facilityId to favorite.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created or restored favorite record.
        /// </returns>
        public async Task<ApiResponse<AddFavoriteResponse>> Process(AddFavoriteRequest request, Guid userId)
        {
            // 1. Initialize validation flags
            bool isPatientValid = true;
            bool isInputValid = true;
            bool isTargetValid = true;
            bool isAlreadyFavorited = false;
            // 2. Retrieve patient entity
            var patient = await RetrievePatient(userId);
            // 3. Validate patient existence
            ValidatePatient(patient, ref isPatientValid);
            // 4. Validate input — exactly one of doctorId or facilityId must be provided
            ValidateInput(request, ref isInputValid);
            // 5. Validate target existence
            isTargetValid = await ValidateTarget(request, isPatientValid, isInputValid);
            // 6. Retrieve existing favorite record
            var existingFavorite = await RetrieveExistingFavorite(patient, request, isPatientValid, isInputValid, isTargetValid);
            // 7. Validate duplicate — not already active
            ValidateDuplicate(existingFavorite, ref isAlreadyFavorited);
            // 8. Execute create or restore
            var favorite = await ExecuteAddOrRestore(patient, request, existingFavorite, isPatientValid, isInputValid, isTargetValid, isAlreadyFavorited);
            // 9. Map to response
            var response = MapToResponse(favorite);
            // 10. Return API response
            return CreateResponse(response, isPatientValid, isInputValid, isTargetValid, isAlreadyFavorited);
        }

        /// <summary>
        /// Retrieves the patient entity by the authenticated user identifier.
        /// </summary>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>Patient entity if found; otherwise null.</returns>
        private async Task<Patient?> RetrievePatient(Guid userId)
        {
            return await _getPatientByUserId.Execute(userId);
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
        /// Validates that exactly one of doctorId or facilityId is provided in the request.
        /// </summary>
        /// <param name="request">The request containing the target identifiers.</param>
        /// <param name="isInputValid">Flag; set to false if both or neither values are provided.</param>
        private void ValidateInput(AddFavoriteRequest request, ref bool isInputValid)
        {
            var hasDoctorId = request.DoctorId.HasValue && request.DoctorId != Guid.Empty;
            var hasFacilityId = request.FacilityId.HasValue && request.FacilityId != Guid.Empty;
            if (hasDoctorId == hasFacilityId)
            {
                isInputValid = false;
            }
        }

        /// <summary>
        /// Validates that the target doctor or facility exists in the database.
        /// </summary>
        /// <param name="request">The request containing the target identifier.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isInputValid">Flag indicating whether the input is valid.</param>
        /// <param name="isTargetValid">Flag; set to false if the target entity does not exist.</param>
        private async Task<bool> ValidateTarget(AddFavoriteRequest request, bool isPatientValid, bool isInputValid)
        {
            if (!isPatientValid || !isInputValid)
            {
                return true;
            }
            if (request.DoctorId.HasValue)
            {
                var doctor = await _getDoctorForBooking.Execute(request.DoctorId.Value);
                return doctor != null;
            }
            if (request.FacilityId.HasValue)
            {
                var facility = await _getFacilityForBooking.Execute(request.FacilityId.Value);
                return facility != null;
            }
            return true;
        }

        /// <summary>
        /// Retrieves an existing favorite record including soft-deleted ones.
        /// </summary>
        /// <param name="patient">The patient entity.</param>
        /// <param name="request">The request containing the target identifier.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isInputValid">Flag indicating whether the input is valid.</param>
        /// <param name="isTargetValid">Flag indicating whether the target is valid.</param>
        /// <returns>Existing favorite entity including deleted ones; otherwise null.</returns>
        private async Task<Favorite?> RetrieveExistingFavorite(Patient? patient, AddFavoriteRequest request, bool isPatientValid, bool isInputValid, bool isTargetValid)
        {
            if (!isPatientValid || !isInputValid || !isTargetValid || patient == null)
            {
                return null;
            }
            return await _getFavoriteByTarget.Execute(patient.Id, request.DoctorId, request.FacilityId);
        }

        /// <summary>
        /// Validates that the target has not already been favorited (active record exists).
        /// </summary>
        /// <param name="existingFavorite">The existing favorite entity to check.</param>
        /// <param name="isAlreadyFavorited">Flag; set to true if an active favorite already exists.</param>
        private void ValidateDuplicate(Favorite? existingFavorite, ref bool isAlreadyFavorited)
        {
            if (existingFavorite != null && !existingFavorite.IsDeleted)
            {
                isAlreadyFavorited = true;
            }
        }

        /// <summary>
        /// Executes the add or restore favorite operation when all validation flags are valid.
        /// </summary>
        /// <param name="patient">The patient entity.</param>
        /// <param name="request">The request containing the target identifier.</param>
        /// <param name="existingFavorite">The existing soft-deleted favorite record, if any.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isInputValid">Flag indicating whether the input is valid.</param>
        /// <param name="isTargetValid">Flag indicating whether the target is valid.</param>
        /// <param name="isAlreadyFavorited">Flag indicating whether the target is already favorited.</param>
        /// <returns>The created or restored favorite entity; otherwise null.</returns>
        private async Task<Favorite?> ExecuteAddOrRestore(
            Patient? patient,
            AddFavoriteRequest request,
            Favorite? existingFavorite,
            bool isPatientValid,
            bool isInputValid,
            bool isTargetValid,
            bool isAlreadyFavorited)
        {
            if (!isPatientValid || !isInputValid || !isTargetValid || isAlreadyFavorited || patient == null)
            {
                return null;
            }
            if (existingFavorite != null && existingFavorite.IsDeleted)
            {
                return await _restoreFavorite.Execute(existingFavorite, patient.Id.ToString());
            }
            var newFavorite = new Favorite
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                DoctorId = request.DoctorId,
                FacilityId = request.FacilityId,
                IsDeleted = false,
                CreateBy = patient.Id.ToString(),
                LastModifiedBy = patient.Id.ToString(),
                CreateDate = DateTimeOffset.UtcNow
            };
            return await _addFavorite.Execute(newFavorite);
        }

        /// <summary>
        /// Maps the favorite entity to <see cref="AddFavoriteResponse"/>.
        /// </summary>
        /// <param name="favorite">The favorite entity to map.</param>
        /// <returns>Mapped <see cref="AddFavoriteResponse"/>; default instance if favorite is null.</returns>
        private AddFavoriteResponse MapToResponse(Favorite? favorite)
        {
            if (favorite == null)
            {
                return new AddFavoriteResponse();
            }
            return new AddFavoriteResponse
            {
                Id = favorite.Id,
                PatientId = favorite.PatientId,
                DoctorId = favorite.DoctorId,
                FacilityId = favorite.FacilityId,
                CreatedAt = favorite.CreateDate
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isInputValid">Flag indicating whether the input is valid.</param>
        /// <param name="isTargetValid">Flag indicating whether the target entity exists.</param>
        /// <param name="isAlreadyFavorited">Flag indicating whether the target is already favorited.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<AddFavoriteResponse> CreateResponse(
            AddFavoriteResponse response,
            bool isPatientValid,
            bool isInputValid,
            bool isTargetValid,
            bool isAlreadyFavorited)
        {
            if (!isPatientValid)
                return ApiResponse<AddFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            if (!isInputValid)
                return ApiResponse<AddFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            if (!isTargetValid)
                return ApiResponse<AddFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            if (isAlreadyFavorited)
                return ApiResponse<AddFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            return ApiResponse<AddFavoriteResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
