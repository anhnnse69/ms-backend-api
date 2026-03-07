using MS.Application.Common.Response;
using MS.Domain.Entities;

namespace MS.Application.Services.AdminServices.GetUserByIdService
{
    /// <summary>
    /// Get user by id service interface
    /// </summary>
    public interface IGetUserByIdService
    {
        /// <summary>
        /// Process get user by id request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<User>> Process(Guid id);
    }
}