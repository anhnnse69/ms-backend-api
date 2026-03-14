using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.DeleteFacilityService
{
    /// <summary>
    /// Delete facility service interface
    /// </summary>
    public interface IDeleteFacilityService
    {
        /// <summary>
        /// Process delete facility request
        /// </summary>
        /// <param name="id">The unique identifier of the facility.</param>
        /// <returns></returns>
        Task<ApiResponse<DeleteFacilityResponse>> Process(Guid id);
    }
}