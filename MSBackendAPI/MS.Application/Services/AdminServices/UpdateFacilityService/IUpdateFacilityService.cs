using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.UpdateFacilityService
{
    /// <summary>
    /// Defines the contract for updating facility information.
    /// </summary>
    public interface IUpdateFacilityService
    {
        /// <summary>
        /// Processes the request to update a facility.
        /// </summary>
        Task<ApiResponse<bool>> Process(Guid id, UpdateFacilityRequest request);
    }
}