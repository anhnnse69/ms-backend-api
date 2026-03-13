using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility;

namespace MS.Application.Services.AdminServices.DeleteFacilityService
{
    /// <summary>
    /// Soft delete facility service implementation
    /// </summary>
    public class DeleteFacilityService : IDeleteFacilityService
    {
        private readonly IGetFacilityById _getFacilityById;
        private readonly IUpdateFacility _updateFacility;

        /// <summary>
        /// Initializes a new instance of the DeleteFacilityService class.
        /// </summary>
        /// <param name="getFacilityById">
        /// Repository used to retrieve facility by identifier.
        /// </param>
        /// <param name="updateFacility">
        /// Repository used to update facility data.
        /// </param>
        public DeleteFacilityService(
            IGetFacilityById getFacilityById,
            IUpdateFacility updateFacility)
        {
            _getFacilityById = getFacilityById;
            _updateFacility = updateFacility;
        }

        /// <summary>
        /// Process soft delete facility request
        /// </summary>
        /// <param name="id">The unique identifier of the facility.</param>
        /// <returns></returns>
        public async Task<ApiResponse<DeleteFacilityResponse>> Process(Guid id)
        {
            // 1. Initialize validation flag
            bool isFacilityExist = true;
            // 2. Retrieve facility by id
            var retrievedFacility = await RetrieveFacilityData(id);
            // 3. Validate retrieved facility data
            ValidateRetrievedData(retrievedFacility, ref isFacilityExist);
            // 4. Perform soft delete operation
            await SoftDeleteFacility(retrievedFacility, isFacilityExist);
            // 5. Create response
            return CreateResponse(isFacilityExist);
        }

        /// <summary>
        /// Retrieve facility by id
        /// </summary>
        /// <param name="id">Facility identifier.</param>
        /// <returns></returns>
        private async Task<Facility?> RetrieveFacilityData(Guid id)
        {
            return await _getFacilityById.Execute(id);
        }

        /// <summary>
        /// Validate facility existence
        /// </summary>
        /// <param name="facility">Retrieved facility entity.</param>
        /// <param name="isFacilityExist">Flag indicating whether facility exists.</param>
        private void ValidateRetrievedData(Facility? facility, ref bool isFacilityExist)
        {
            if (facility == null)
            {
                isFacilityExist = false;
            }
        }

        /// <summary>
        /// Perform soft delete operation for facility
        /// </summary>
        /// <param name="facility">Facility entity.</param>
        /// <param name="isFacilityExist">Flag indicating whether facility exists.</param>
        private async Task SoftDeleteFacility(Facility? facility, bool isFacilityExist)
        {
            if (!isFacilityExist)
                return;
            facility!.IsDeleted = true;
            facility.DeletedAt = DateTimeOffset.UtcNow;
            facility.DeletedBy = "system";
            await _updateFacility.Execute(facility);
        }

        /// <summary>
        /// Create response for delete facility request
        /// </summary>
        /// <param name="isFacilityExist">Flag indicating whether facility exists.</param>
        /// <returns></returns>
        private ApiResponse<DeleteFacilityResponse> CreateResponse(bool isFacilityExist)
        {
            if (!isFacilityExist)
            {
                return ApiResponse<DeleteFacilityResponse>.Fail(
                    MessageCode.APP_MESSAGE_4008.ToString()
                );
            }
            return ApiResponse<DeleteFacilityResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new DeleteFacilityResponse(true)
            );
        }
    }
}