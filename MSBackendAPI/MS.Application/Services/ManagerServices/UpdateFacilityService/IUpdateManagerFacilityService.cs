using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.UpdateFacilityService
{
    /// <summary>
    /// Service responsible for updating facility information by manager.
    /// </summary>
    public interface IUpdateManagerFacilityService
    {
        /// <summary>
        /// Processes the request to update facility information.
        /// </summary>
        /// <param name="request">
        /// Request object containing facility identifier and updated facility data.
        /// </param>
        /// <returns>
        /// API response indicating whether the update operation was successful.
        /// </returns>
        Task<ApiResponse<bool>> Process(UpdateManagerFacilityRequest request);
    }
}