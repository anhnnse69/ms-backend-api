using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteById;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId;
using MS.Infrastructure.Repositories.PatientRepositories.SoftDeleteFavorite;

namespace MS.Application.Services.PatientServices.DeleteFavoriteService
{
    /// <summary>
    /// Handles the business logic for soft-deleting a favorite record.
    /// </summary>
    public class DeleteFavoriteService : IDeleteFavoriteService
    {
        private readonly IGetPatientByUserId _getPatientByUserId;
        private readonly IGetFavoriteById _getFavoriteById;
        private readonly ISoftDeleteFavorite _softDeleteFavorite;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFavoriteService"/> class.
        /// </summary>
        /// <param name="getPatientByUserId">Repository responsible for retrieving patient by user identifier.</param>
        /// <param name="getFavoriteById">Repository responsible for retrieving a favorite by identifier.</param>
        /// <param name="softDeleteFavorite">Repository responsible for performing soft delete on a favorite.</param>
        public DeleteFavoriteService(
            IGetPatientByUserId getPatientByUserId,
            IGetFavoriteById getFavoriteById,
            ISoftDeleteFavorite softDeleteFavorite)
        {
            _getPatientByUserId = getPatientByUserId;
            _getFavoriteById = getFavoriteById;
            _softDeleteFavorite = softDeleteFavorite;
        }

        /// <summary>
        /// Processes the request to soft-delete a favorite record.
        /// </summary>
        /// <param name="favoriteId">The unique identifier of the favorite record to delete.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> indicating whether the deletion succeeded.
        /// </returns>
        public async Task<ApiResponse<DeleteFavoriteResponse>> Process(Guid favoriteId, Guid userId)
        {
            // 1. Initialize validation flags
            bool isPatientValid = true;
            bool isFavoriteValid = true;
            bool isOwnerValid = true;
            // 2. Retrieve patient entity
            var patient = await RetrievePatient(userId);
            // 3. Retrieve favorite entity
            var favorite = await RetrieveFavorite(favoriteId);
            // 4. Validate patient existence
            ValidatePatient(patient, ref isPatientValid);
            // 5. Validate favorite existence and active status
            ValidateFavorite(favorite, ref isFavoriteValid);
            // 6. Validate ownership
            ValidateOwnership(favorite, patient, ref isOwnerValid);
            // 7. Execute soft delete
            await ExecuteSoftDelete(favorite, patient, isPatientValid, isFavoriteValid, isOwnerValid);
            // 8. Map to response
            var response = MapToResponse(isPatientValid, isFavoriteValid, isOwnerValid);
            // 9. Return API response
            return CreateResponse(response, isPatientValid, isFavoriteValid, isOwnerValid);
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
        /// Retrieves the favorite entity by its unique identifier.
        /// </summary>
        /// <param name="favoriteId">The unique identifier of the favorite record.</param>
        /// <returns>Favorite entity if found and active; otherwise null.</returns>
        private async Task<Favorite?> RetrieveFavorite(Guid favoriteId)
        {
            return await _getFavoriteById.Execute(favoriteId);
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
        /// Validates whether the favorite entity exists and is not already deleted.
        /// </summary>
        /// <param name="favorite">The favorite entity to validate.</param>
        /// <param name="isFavoriteValid">Flag; set to false if favorite is null or already deleted.</param>
        private void ValidateFavorite(Favorite? favorite, ref bool isFavoriteValid)
        {
            if (favorite == null || favorite.IsDeleted)
            {
                isFavoriteValid = false;
            }
        }

        /// <summary>
        /// Validates whether the authenticated patient owns the favorite record.
        /// </summary>
        /// <param name="favorite">The favorite entity to check ownership of.</param>
        /// <param name="patient">The patient entity to check against.</param>
        /// <param name="isOwnerValid">Flag; set to false if the patient does not own the favorite.</param>
        private void ValidateOwnership(Favorite? favorite, Patient? patient, ref bool isOwnerValid)
        {
            if (favorite != null && patient != null && favorite.PatientId != patient.Id)
            {
                isOwnerValid = false;
            }
        }

        /// <summary>
        /// Executes the soft delete operation when all validation flags are valid.
        /// </summary>
        /// <param name="favorite">The favorite entity to soft delete.</param>
        /// <param name="patient">The patient performing the deletion.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isFavoriteValid">Flag indicating whether the favorite is valid.</param>
        /// <param name="isOwnerValid">Flag indicating whether ownership is valid.</param>
        private async Task ExecuteSoftDelete(
            Favorite? favorite,
            Patient? patient,
            bool isPatientValid,
            bool isFavoriteValid,
            bool isOwnerValid)
        {
            if (!isPatientValid || !isFavoriteValid || !isOwnerValid || favorite == null || patient == null)
            {
                return;
            }
            await _softDeleteFavorite.Execute(favorite, patient.Id.ToString());
        }

        /// <summary>
        /// Maps the deletion result to <see cref="DeleteFavoriteResponse"/>.
        /// </summary>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isFavoriteValid">Flag indicating whether the favorite is valid.</param>
        /// <param name="isOwnerValid">Flag indicating whether ownership is valid.</param>
        /// <returns>Mapped <see cref="DeleteFavoriteResponse"/>.</returns>
        private DeleteFavoriteResponse MapToResponse(bool isPatientValid, bool isFavoriteValid, bool isOwnerValid)
        {
            return new DeleteFavoriteResponse
            {
                IsDeleted = isPatientValid && isFavoriteValid && isOwnerValid
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <param name="isFavoriteValid">Flag indicating whether the favorite exists and is active.</param>
        /// <param name="isOwnerValid">Flag indicating whether the patient owns the favorite.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<DeleteFavoriteResponse> CreateResponse(
            DeleteFavoriteResponse response,
            bool isPatientValid,
            bool isFavoriteValid,
            bool isOwnerValid)
        {
            if (!isPatientValid)
                return ApiResponse<DeleteFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            if (!isFavoriteValid)
                return ApiResponse<DeleteFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4012.ToString());
            if (!isOwnerValid)
                return ApiResponse<DeleteFavoriteResponse>.Fail(MessageCode.APP_MESSAGE_4014.ToString());
            return ApiResponse<DeleteFavoriteResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
