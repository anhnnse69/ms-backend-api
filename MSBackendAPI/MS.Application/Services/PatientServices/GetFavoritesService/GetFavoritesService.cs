using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientFavorites;

namespace MS.Application.Services.PatientServices.GetFavoritesService
{
    /// <summary>
    /// Handles the business logic for retrieving the patient's favorites list with pagination and filtering.
    /// </summary>
    public class GetFavoritesService : IGetFavoritesService
    {
        private readonly IGetPatientByUserId _getPatientByUserId;
        private readonly IGetPatientFavorites _getPatientFavorites;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFavoritesService"/> class.
        /// </summary>
        /// <param name="getPatientByUserId">Repository responsible for retrieving patient by user identifier.</param>
        /// <param name="getPatientFavorites">Repository responsible for retrieving the patient's favorites.</param>
        public GetFavoritesService(
            IGetPatientByUserId getPatientByUserId,
            IGetPatientFavorites getPatientFavorites)
        {
            _getPatientByUserId = getPatientByUserId;
            _getPatientFavorites = getPatientFavorites;
        }

        /// <summary>
        /// Processes the request to retrieve the patient's paginated favorites list.
        /// </summary>
        /// <param name="request">The query parameters including page, size, and type filter.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the paginated list of favorites.
        /// </returns>
        public async Task<ApiResponse<List<GetFavoritesResponse>>> Process(GetFavoritesRequest request, Guid userId)
        {
            // 1. Initialize validation flags
            bool isPatientValid = true;
            // 2. Retrieve patient entity
            var patient = await RetrievePatient(userId);
            // 3. Validate patient existence
            ValidatePatient(patient, ref isPatientValid);
            // 4. Retrieve favorites from repository
            var (favorites, total) = await RetrieveFavorites(patient, request, isPatientValid);
            // 5. Map to response
            var response = MapToResponse(favorites, isPatientValid);
            // 6. Return API response
            return CreateResponse(response, total, request.Page, request.Size, isPatientValid);
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
        /// Retrieves the paginated favorites list from the repository.
        /// </summary>
        /// <param name="patient">The patient entity.</param>
        /// <param name="request">The query parameters for filtering and pagination.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <returns>Tuple of favorites list and total count; empty list if patient is invalid.</returns>
        private async Task<(List<Favorite> favorites, int total)> RetrieveFavorites(
            Patient? patient,
            GetFavoritesRequest request,
            bool isPatientValid)
        {
            if (!isPatientValid || patient == null)
            {
                return (new List<Favorite>(), 0);
            }
            return await _getPatientFavorites.Execute(patient.Id, request.Type, request.Page, request.Size);
        }

        /// <summary>
        /// Maps the list of favorite entities to <see cref="GetFavoritesResponse"/> items.
        /// </summary>
        /// <param name="favorites">The list of favorite entities to map.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <returns>Mapped list of <see cref="GetFavoritesResponse"/>; empty list if patient is invalid.</returns>
        private List<GetFavoritesResponse> MapToResponse(List<Favorite> favorites, bool isPatientValid)
        {
            if (!isPatientValid)
            {
                return new List<GetFavoritesResponse>();
            }
            return favorites.Select(f =>
            {
                var isDoctor = f.DoctorId.HasValue;
                return new GetFavoritesResponse
                {
                    Id = f.Id,
                    FavoriteType = isDoctor ? "Doctor" : "Facility",
                    DoctorId = f.DoctorId,
                    FacilityId = f.FacilityId,
                    Name = isDoctor
                        ? (f.Doctor?.FullName ?? string.Empty)
                        : (f.Facility?.NameVi ?? string.Empty),
                    Avatar = isDoctor ? f.Doctor?.AvatarUrl : f.Facility?.LogoUrl,
                    Specialty = isDoctor ? f.Doctor?.Specialty?.NameVi : null,
                    Address = isDoctor ? null : f.Facility?.Address,
                    SavedAt = f.CreateDate
                };
            }).ToList();
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags and pagination metadata.
        /// </summary>
        /// <param name="response">The mapped list of favorites.</param>
        /// <param name="total">The total number of records.</param>
        /// <param name="page">The current page index.</param>
        /// <param name="size">The number of records per page.</param>
        /// <param name="isPatientValid">Flag indicating whether the patient is valid.</param>
        /// <returns>
        /// Success response with pagination metadata if valid; otherwise a failure response.
        /// </returns>
        private ApiResponse<List<GetFavoritesResponse>> CreateResponse(
            List<GetFavoritesResponse> response,
            int total,
            int page,
            int size,
            bool isPatientValid)
        {
            if (!isPatientValid)
                return ApiResponse<List<GetFavoritesResponse>>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            return ApiResponse<List<GetFavoritesResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response,
                new MetaResponse(page, size, total));
        }
    }
}
