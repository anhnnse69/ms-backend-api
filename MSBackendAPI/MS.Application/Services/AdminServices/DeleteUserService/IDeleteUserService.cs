using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.DeleteUserService
{
    /// <summary>
    /// Interface for delete user service.
    /// </summary>
    public interface IDeleteUserService
    {
        /// <summary>
        /// Process delete user request.
        /// </summary>
        Task<ApiResponse<DeleteUserResponse>> Process(Guid id);
    }
}