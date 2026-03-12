using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.CreateUser
{
    /// <summary>
    /// Defines the contract for the create user service.
    /// </summary>
    public interface ICreateUserService
    {
        /// <summary>
        /// Processes the request to create a new user.
        /// </summary>
        /// <param name="request">
        /// The request containing the user information required for creation.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the ID of the newly created user.
        /// </returns>
        Task<ApiResponse<Guid>> Process(CreateUserRequest request);
    }
}