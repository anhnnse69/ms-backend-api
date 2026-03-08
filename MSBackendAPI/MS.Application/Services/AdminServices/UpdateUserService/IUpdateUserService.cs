using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.UpdateUserService
{
    /// <summary>
    /// Defines the contract for the update user service.
    /// </summary>
    public interface IUpdateUserService
    {
        /// <summary>
        /// Processes the request to update a user.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to update.
        /// </param>
        /// <param name="request">
        /// The request containing updated user information.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Boolean}"/> indicating the result of the update operation.
        /// </returns>
        Task<ApiResponse<bool>> Process(Guid id, UpdateUserRequest request);
    }
}