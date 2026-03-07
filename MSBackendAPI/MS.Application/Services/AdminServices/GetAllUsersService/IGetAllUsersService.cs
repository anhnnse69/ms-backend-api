using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.GetAllUsersService
{
    /// <summary>
    /// Get all users service interface
    /// </summary>
    public interface IGetAllUsersService
    {
        /// <summary>
        /// Process get all users request
        /// </summary>
        /// <returns></returns>
        Task<ApiResponse<IEnumerable<GetAllUsersResponse>>> Process(int page, int size);
    }
}