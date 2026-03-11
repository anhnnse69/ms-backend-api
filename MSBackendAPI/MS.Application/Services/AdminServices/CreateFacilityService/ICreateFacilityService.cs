using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.CreateFacilityService
{
    /// <summary>
    /// Defines the contract for the create facility service.
    /// </summary>
    public interface ICreateFacilityService
    {
        /// <summary>
        /// Processes the request to create a new facility.
        /// </summary>
        /// <param name="request">
        /// The request containing facility information.
        /// </param>
        /// <returns>
        /// An ApiResponse containing the ID of the newly created facility.
        /// </returns>
        Task<ApiResponse<Guid>> Process(CreateFacilityRequest request);
    }
}