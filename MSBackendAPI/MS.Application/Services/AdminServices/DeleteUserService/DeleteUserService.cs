using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserById;
using MS.Infrastructure.Repositories.PatientRepositories.UpdateUser;

namespace MS.Application.Services.AdminServices.DeleteUserService
{
    /// <summary>
    /// Service for deleting user (soft delete).
    /// </summary>
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;

        public DeleteUserService(IGetUserById getUserById, IUpdateUser updateUser)
        {
            _getUserById = getUserById;
            _updateUser = updateUser;
        }

        /// <summary>
        /// Process delete user request.
        /// </summary>
        public async Task<ApiResponse<DeleteUserResponse>> Process(Guid id)
        {
            // 1. Initialize validation flags
            bool isUserFound = true;
            // 2. Retrieve user by id
            var user = await RetrieveUser(id);
            // 3. Validate retrieved user
            ValidateUser(user, ref isUserFound);
            // 4. Create response
            return await CreateResponse(user, isUserFound);
        }

        /// <summary>
        /// Retrieve user from database.
        /// </summary>
        private async Task<User> RetrieveUser(Guid id)
        {
            return await _getUserById.Execute(id);
        }

        /// <summary>
        /// Validate retrieved user.
        /// </summary>
        private void ValidateUser(User user, ref bool isUserFound)
        {
            // Check if user exists
            if (user == null)
            {
                isUserFound = false;
            }
        }

        /// <summary>
        /// Create response after processing delete user.
        /// </summary>
        private async Task<ApiResponse<DeleteUserResponse>> CreateResponse(User user, bool isUserFound)
        {
            // User not found
            if (!isUserFound)
            {
                return ApiResponse<DeleteUserResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }
            // Soft delete user using ISoftDeletable
            user.IsDeleted = true;
            user.DeletedAt = DateTimeOffset.UtcNow;
            user.DeletedBy = "system";
            user.LastModifiedBy = "system";
            user.LastModifiedDate = DateTimeOffset.UtcNow;
            // Update user in database
            await _updateUser.Execute(user);
            // Create response object
            var response = new DeleteUserResponse
            {
                IsDeleted = true
            };
            // Return success response
            return ApiResponse<DeleteUserResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response
            );
        }
    }
}