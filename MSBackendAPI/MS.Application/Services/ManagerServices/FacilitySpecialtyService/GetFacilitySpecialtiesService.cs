using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetSpecialtiesByFacilityId;

namespace MS.Application.Services.ManagerServices.FacilitySpecialtyService
{
    /// <summary>
    /// Implementation for getting specialties available at a specific facility
    /// </summary>
    public class GetFacilitySpecialtiesService : IGetFacilitySpecialtiesService
    {
        private readonly IGetSpecialtiesByFacilityId _getSpecialtiesByFacilityId;

        /// <summary>
        /// Constructor for GetFacilitySpecialtiesService
        /// </summary>
        /// <param name="getSpecialtiesByFacilityId"></param>
        public GetFacilitySpecialtiesService(IGetSpecialtiesByFacilityId getSpecialtiesByFacilityId)
        {
            _getSpecialtiesByFacilityId = getSpecialtiesByFacilityId;
        }

        /// <summary>
        /// Process the request to get facility specialties without conditional blocks in the main flow
        /// </summary>
        /// <param name="request">The request containing FacilityId</param>
        /// <returns>ApiResponse containing list of specialties</returns>
        public async Task<ApiResponse<List<FacilitySpecialtyResponse>>> Process(GetFacilitySpecialtiesRequest request)
        {
            // 1. Initialize validation flags
            bool isRetrievedDataValid = true;

            // 2. Retrieve data
            var retrievedData = await RetrieveData(request.FacilityId);

            // 3. Validate retrieved data
            ValidateRetrievedData(retrievedData, ref isRetrievedDataValid);

            // 4. Map data to DTO
            var mappedData = MapToResponse(retrievedData);

            // 5. Create response
            return CreateResponse(mappedData, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve facility specialties data from database
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <returns>List of FacilitySpecialty entities</returns>
        private async Task<List<FacilitySpecialty>> RetrieveData(Guid facilityId)
        {
            return await _getSpecialtiesByFacilityId.Execute(facilityId);
        }

        /// <summary>
        /// Validate the retrieved data
        /// </summary>
        /// <param name="retrievedData">Data retrieved from DB</param>
        /// <param name="isRetrievedDataValid">Validation flag</param>
        private void ValidateRetrievedData(List<FacilitySpecialty> retrievedData, ref bool isRetrievedDataValid)
        {
            if (retrievedData == null)
            {
                isRetrievedDataValid = false;
            }
        }

        /// <summary>
        /// Map entity data to response DTOs
        /// </summary>
        /// <param name="retrievedData">List of entities</param>
        /// <returns>List of mapped DTOs</returns>
        private List<FacilitySpecialtyResponse> MapToResponse(List<FacilitySpecialty> retrievedData)
        {
            var result = new List<FacilitySpecialtyResponse>();

            if (retrievedData != null && retrievedData.Any())
            {
                foreach (var item in retrievedData)
                {
                    if (item.Specialty != null)
                    {
                        result.Add(new FacilitySpecialtyResponse
                        {                           
                            SpecialtyId = item.Specialty.Id,
                            NameVi = item.Specialty.NameVi,
                            NameEn = item.Specialty.NameEn,
                            DescriptionVi = item.Specialty.DescriptionVi,
                            DescriptionEn = item.Specialty.DescriptionEn,
                            IconUrl = item.Specialty.IconUrl
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Create the final API response
        /// </summary>
        /// <param name="mappedData">Mapped DTO list</param>
        /// <param name="isRetrievedDataValid">Validation flag status</param>
        /// <returns>Formatted ApiResponse</returns>
        private ApiResponse<List<FacilitySpecialtyResponse>> CreateResponse(List<FacilitySpecialtyResponse> mappedData, bool isRetrievedDataValid)
        {
            if (!isRetrievedDataValid)
            {
                return ApiResponse<List<FacilitySpecialtyResponse>>.Fail(MessageCode.APP_MESSAGE_4008.ToString());
            }

            return ApiResponse<List<FacilitySpecialtyResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                mappedData
            );
        }
    }
}
