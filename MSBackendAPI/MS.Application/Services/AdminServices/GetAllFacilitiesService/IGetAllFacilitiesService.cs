using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.GetAllFacilitiesService
{
    /// <summary>
    /// Get all facilities service interface
    /// </summary>
    public interface IGetAllFacilitiesService
    {
        /// <summary>
        /// Process get all facilities request with pagination
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns></returns>
        Task<ApiResponse<IEnumerable<GetAllFacilitiesResponse>>> Process(int page, int size);
    }
}