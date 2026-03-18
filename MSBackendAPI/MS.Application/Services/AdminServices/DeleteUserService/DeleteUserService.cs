using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateUser;

namespace MS.Application.Services.AdminServices.DeleteUserService
{
    /// <summary>
    /// Soft delete user service implementation
    /// </summary>
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;

        /// <summary>
        /// Initializes a new instance of the DeleteUserService class.
        /// </summary>
        /// <param name="getUserById">
        /// Repository used to retrieve user by identifier.
        /// </param>
        /// <param name="updateUser">
        /// Repository used to update user data.
        /// </param>
        public DeleteUserService(
            IGetUserById getUserById,
            IUpdateUser updateUser)
        {
            _getUserById = getUserById;
            _updateUser = updateUser;
        }

        /// <summary>
        /// Process soft delete user request
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns></returns>
        public async Task<ApiResponse<DeleteUserResponse>> Process(Guid id)
        {
            // 1. Initialize validation flag
            bool isUserExist = true;
            // 2. Retrieve user by id
            var retrievedUser = await RetrieveUserData(id);
            // 3. Validate retrieved user data
            ValidateRetrievedData(retrievedUser, ref isUserExist);
            // 4. Perform soft delete operation
            await SoftDeleteUser(retrievedUser, isUserExist);
            // 5. Create response
            return CreateResponse(isUserExist);
        }

        /// <summary>
        /// Retrieve user by id
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <returns></returns>
        private async Task<User?> RetrieveUserData(Guid id)
        {
            return await _getUserById.Execute(id);
        }

        /// <summary>
        /// Validate user existence
        /// </summary>
        /// <param name="user">Retrieved user entity.</param>
        /// <param name="isUserExist">Flag indicating whether user exists.</param>
        private void ValidateRetrievedData(User? user, ref bool isUserExist)
        {
            if (user == null)
            {
                isUserExist = false;
            }
        }

        /// <summary>
        /// Perform soft delete operation for user
        /// </summary>
        /// <param name="user">User entity.</param>
        /// <param name="isUserExist">Flag indicating whether user exists.</param>
        private async Task SoftDeleteUser(User? user, bool isUserExist)
        {
            if (!isUserExist)
                return;
            user!.IsDeleted = true;
            user.DeletedAt = DateTimeOffset.UtcNow;
            user.DeletedBy = "system";
            user.LastModifiedBy = "system";
            user.LastModifiedDate = DateTimeOffset.UtcNow;
            await _updateUser.Execute(user);
        }

        /// <summary>
        /// Create response for delete user request
        /// </summary>
        /// <param name="isUserExist">Flag indicating whether user exists.</param>
        /// <returns></returns>
        private ApiResponse<DeleteUserResponse> CreateResponse(bool isUserExist)
        {
            if (!isUserExist)
            {
                return ApiResponse<DeleteUserResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }
            return ApiResponse<DeleteUserResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new DeleteUserResponse(true)
            );
        }
    }
}