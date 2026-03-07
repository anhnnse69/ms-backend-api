using MS.Application.Common.Response;
using MS.Domain.Entities;

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
        Task<ApiResponse<List<User>>> Process();
    }
}