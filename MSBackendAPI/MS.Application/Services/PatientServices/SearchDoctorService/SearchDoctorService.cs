using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.SearchDoctor;

namespace MS.Application.Services.PatientServices.SearchDoctorService
{
    /// <summary>
    /// Service responsible for handling the search doctor business logic.
    /// </summary>
    public class SearchDoctorService : ISearchDoctorService
    {
        private readonly ISearchDoctor _searchDoctorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDoctorService"/> class.
        /// </summary>
        /// <param name="searchDoctorRepo">Repository responsible for doctor search data access.</param>
        public SearchDoctorService(ISearchDoctor searchDoctorRepo)
        {
            _searchDoctorRepo = searchDoctorRepo;
        }

        /// <summary>
        /// Processes the request to search doctors by keyword, specialty, facility, and location.
        /// </summary>
        /// <param name="keyword">Optional keyword to search by doctor name or facility name.</param>
        /// <param name="specialtyId">Optional specialty identifier to filter results.</param>
        /// <param name="facilityId">Optional facility identifier to filter results.</param>
        /// <param name="location">Optional city or address to filter by location.</param>
        /// <param name="page">Current page index.</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a paginated list of doctor search results.
        /// </returns>
        public async Task<ApiResponse<List<SearchDoctorResponse>>> Process(
            string? keyword,
            Guid? specialtyId,
            Guid? facilityId,
            string? location,
            int page,
            int size)
        {
            // 1. Initialize validation flags
            bool isPaginationValid = true;
            // 2. Retrieve data from repositories
            var (doctors, total) = await RetrieveDoctors(keyword, specialtyId, facilityId, location, page, size);
            // 3. Validate retrieved data
            ValidatePagination(page, size, ref isPaginationValid);
            // 4. Map to response
            var response = MapToResponse(doctors, isPaginationValid);
            // 5. Return API response
            return CreateResponse(response, total, page, size, isPaginationValid);
        }

        /// <summary>
        /// Retrieves the paginated list of doctors matching the specified search criteria.
        /// </summary>
        /// <param name="keyword">Optional keyword for doctor name or facility name search.</param>
        /// <param name="specialtyId">Optional specialty identifier filter.</param>
        /// <param name="facilityId">Optional facility identifier filter.</param>
        /// <param name="location">Optional city or address filter.</param>
        /// <param name="page">Current page index.</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>Tuple of doctor list and total count matching the criteria.</returns>
        private async Task<(List<Doctor> doctors, int total)> RetrieveDoctors(
            string? keyword,
            Guid? specialtyId,
            Guid? facilityId,
            string? location,
            int page,
            int size)
        {
            return await _searchDoctorRepo.Execute(keyword, specialtyId, facilityId, location, page, size);
        }

        /// <summary>
        /// Validates whether the pagination parameters are valid.
        /// </summary>
        /// <param name="page">The page index to validate.</param>
        /// <param name="size">The page size to validate.</param>
        /// <param name="isPaginationValid">Flag; set to false if page or size is less than 1.</param>
        private void ValidatePagination(int page, int size, ref bool isPaginationValid)
        {
            if (page < 1 || size < 1)
            {
                isPaginationValid = false;
            }
        }

        /// <summary>
        /// Maps a list of doctor entities to a list of <see cref="SearchDoctorResponse"/>.
        /// </summary>
        /// <param name="doctors">The list of doctor entities to map.</param>
        /// <param name="isPaginationValid">Flag indicating whether pagination is valid.</param>
        /// <returns>Mapped list of <see cref="SearchDoctorResponse"/>; empty list if data is invalid.</returns>
        private List<SearchDoctorResponse> MapToResponse(List<Doctor> doctors, bool isPaginationValid)
        {
            if (!isPaginationValid || doctors == null)
            {
                return new List<SearchDoctorResponse>();
            }
            return doctors.Select(d => new SearchDoctorResponse
            {
                Id = d.Id,
                FullName = d.FullName,
                Avatar = d.AvatarUrl,
                ExperienceYears = d.YearsOfExperience,
                AverageRating = d.AverageRating,
                Specialty = d.Specialty == null ? new SearchDoctorSpecialtyResponse() : new SearchDoctorSpecialtyResponse
                {
                    Id = d.Specialty.Id,
                    Name = d.Specialty.NameVi
                },
                Facilities = d.Facilities.Select(df => new SearchDoctorFacilityResponse
                {
                    Id = df.FacilityId,
                    Name = df.Facility.NameVi,
                    Address = df.Facility.Address,
                    City = df.Facility.City
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags and pagination metadata.
        /// </summary>
        /// <param name="response">The mapped list of doctor search results.</param>
        /// <param name="total">The total number of records matching the search criteria.</param>
        /// <param name="page">The current page index.</param>
        /// <param name="size">The number of records per page.</param>
        /// <param name="isPaginationValid">Flag indicating whether pagination parameters are valid.</param>
        /// <returns>
        /// Success response with pagination metadata if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<List<SearchDoctorResponse>> CreateResponse(
            List<SearchDoctorResponse> response,
            int total,
            int page,
            int size,
            bool isPaginationValid)
        {
            if (!isPaginationValid)
                return ApiResponse<List<SearchDoctorResponse>>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            return ApiResponse<List<SearchDoctorResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response,
                new MetaResponse(page, size, total));
        }
    }
}
