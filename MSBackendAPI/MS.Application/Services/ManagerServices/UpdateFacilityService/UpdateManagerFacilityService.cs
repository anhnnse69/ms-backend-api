using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility;

namespace MS.Application.Services.ManagerServices.UpdateFacilityService
{
    /// <summary>
    /// Service responsible for updating facility information managed by a manager.
    /// </summary>
    public class UpdateManagerFacilityService : IUpdateManagerFacilityService
    {
        private readonly IGetFacilityById _getFacilityById;
        private readonly IUpdateFacility _updateFacility;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateManagerFacilityService"/> class.
        /// </summary>
        /// <param name="getFacilityById">
        /// Repository used to retrieve facility information by its identifier.
        /// </param>
        /// <param name="updateFacility">
        /// Repository used to persist updated facility information to the database.
        /// </param>
        public UpdateManagerFacilityService(
            IGetFacilityById getFacilityById,
            IUpdateFacility updateFacility)
        {
            _getFacilityById = getFacilityById;
            _updateFacility = updateFacility;
        }

        /// <summary>
        /// Processes the request to update facility information.
        /// </summary>
        /// <param name="request">
        /// Request object containing facility identifier and updated facility data.
        /// </param>
        /// <returns>
        /// API response indicating whether the update operation was successful.
        /// </returns>
        public async Task<ApiResponse<bool>> Process(UpdateManagerFacilityRequest request)
        {
            // 1. Retrieve facility from database using the provided identifier
            var facility = await _getFacilityById.Execute(request.Id);
            // 2. Map updated data from request to facility entity
            MapFacilityData(request, facility);
            // 3. Persist updated facility data to database
            await UpdateFacility(facility);
            // 4. Create API response based on retrieved facility result
            return CreateResponse(facility);
        }

        /// <summary>
        /// Maps the request data to the facility entity.
        /// This method updates the facility properties if the facility exists.
        /// </summary>
        /// <param name="request">
        /// Request containing updated facility information.
        /// </param>
        /// <param name="facility">
        /// Facility entity retrieved from database.
        /// </param>
        private void MapFacilityData(UpdateManagerFacilityRequest request, Facility? facility)
        {
            if (facility == null) return;
            facility.NameVi = request.NameVi;
            facility.NameEn = request.NameEn;
            facility.DescriptionVi = request.DescriptionVi;
            facility.DescriptionEn = request.DescriptionEn;
            facility.LogoUrl = request.LogoUrl;
            facility.Address = request.Address;
            facility.Phone = request.Phone;
            facility.Email = request.Email;
            facility.City = request.City;
            facility.LastModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the facility entity in the database.
        /// The update operation will only be executed if the facility exists.
        /// </summary>
        /// <param name="facility">
        /// Facility entity containing updated data.
        /// </param>
        private async Task UpdateFacility(Facility? facility)
        {
            if (facility == null) return;
            await _updateFacility.Execute(facility);
        }

        /// <summary>
        /// Creates the API response for the update operation.
        /// </summary>
        /// <param name="facility">
        /// Facility entity retrieved from database.
        /// </param>
        /// <returns>
        /// Success response if the facility exists and update is performed;
        /// otherwise, failure response indicating the facility was not found.
        /// </returns>
        private ApiResponse<bool> CreateResponse(Facility? facility)
        {
            if (facility == null)
            {
                return ApiResponse<bool>.Fail(
                    MessageCode.APP_MESSAGE_4008.ToString());
            }
            return ApiResponse<bool>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                true);
        }
    }
}
