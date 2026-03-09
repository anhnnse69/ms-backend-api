using MS.Application.Common.Response;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorByFacility;

namespace MS.Application.Services.DoctorsByFacilityService
{
    /// <summary>
    /// Service responsible for retrieving doctors by facility
    /// </summary>
    public class DoctorByFacilityService : IDoctorByFacilityService
    {
        private readonly IGetDoctorByFacility _getDoctorsByFacility;
        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="getDoctorsByFacility">
        /// Repository used to retrieve doctors by facility identifier
        /// </param>
        public DoctorByFacilityService(IGetDoctorByFacility getDoctorsByFacility)
        {
            _getDoctorsByFacility = getDoctorsByFacility;
        }
        /// <summary>
        /// Process request to retrieve doctors belonging to a specific facility
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of doctors</returns>
        public async Task<ApiResponse<List<DoctorListResponse>>> Process(Guid facilityId, int page, int size)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Retrieve doctors from repository
            var (doctors, total) = await _getDoctorsByFacility.Execute(facilityId, page, size);
            // 3. Validate retrieved data
            ValidateRetrievedData(doctors, ref isDataValid);
            // 4. Create API response
            return CreateResponse(doctors, page, size, total, isDataValid);
        }
        /// <summary>
        /// Validate retrieved doctors data
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateRetrievedData(List<Doctor> doctors, ref bool isDataValid)
        {
            if (doctors == null || !doctors.Any())
            {
                isDataValid = false;
            }
        }
        /// <summary>
        /// Create API response from doctor entities
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <param name="total">Total number of records</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing doctor list and pagination metadata</returns>
        private ApiResponse<List<DoctorListResponse>> CreateResponse(
            List<Doctor> doctors,
            int page,
            int size,
            int total,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<List<DoctorListResponse>>.Fail(
                    MessageCode.APP_MESSAGE_4004.ToString()
                );
            }
            // Map doctor entities to response model
            var mappedDoctors = MapDoctors(doctors);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<List<DoctorListResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                mappedDoctors,
                meta
            );
        }
        /// <summary>
        /// Map doctor entities to response model
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <returns>List of doctor response models</returns>
        private List<DoctorListResponse> MapDoctors(List<Doctor> doctors)
        {
            return doctors.Select(d => new DoctorListResponse
            {
                Id = d.Id,
                DisplayName = d.DisplayName,
                FullName = d.FullName,
                AcademicTitleVi = d.AcademicTitleVi,
                AvatarUrl = d.AvatarUrl,
                YearsOfExperience = d.YearsOfExperience,
                AverageRating = d.AverageRating,
                SpecialtyName = d.Specialty.NameVi
            }).ToList();
        }
    }
}