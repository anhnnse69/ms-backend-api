using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateUser;

namespace MS.Application.Services.AdminServices.UpdateUserService
{
    /// <summary>
    /// Provides an implementation of the IUpdateUserService interface
    /// for handling user update operations.
    /// </summary>
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;

        /// <summary>
        /// Initializes a new instance of the UpdateUserService class.
        /// </summary>
        /// <param name="getUserById">
        /// Repository used to retrieve user information by ID.
        /// </param>
        /// <param name="updateUser">
        /// Repository responsible for updating user data.
        /// </param>
        public UpdateUserService(IGetUserById getUserById, IUpdateUser updateUser)
        {
            _getUserById = getUserById;
            _updateUser = updateUser;
        }

        /// <summary>
        /// Processes the update user request.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to update.
        /// </param>
        /// <param name="request">
        /// The request object containing updated user information.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Boolean}"/> indicating whether the update operation was successful.
        /// </returns>
        public async Task<ApiResponse<bool>> Process(Guid id, UpdateUserRequest request)
        {
            // 1. Initialize validation flags
            bool isUserFound = true;
            // 2. Retrieve user by id
            var user = await RetrieveUser(id);
            // 3. Validate retrieved user data
            ValidateUser(user, ref isUserFound);
            // 4. Create response
            return await CreateResponse(user, request, isUserFound);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">
        /// The user ID.
        /// </param>
        /// <returns>
        /// A <see cref="User"/> entity if found; otherwise null.
        /// </returns>
        private async Task<User> RetrieveUser(Guid id)
        {
            return await _getUserById.Execute(id);
        }

        /// <summary>
        /// Validates whether the user exists.
        /// </summary>
        /// <param name="user">
        /// The retrieved user entity.
        /// </param>
        /// <param name="isUserFound">
        /// A flag indicating whether the user exists in the system.
        /// </param>
        private void ValidateUser(User user, ref bool isUserFound)
        {
            if (user == null)
                isUserFound = false;
        }

        /// <summary>
        /// Creates the final API response after processing the update operation.
        /// </summary>
        /// <param name="user">
        /// The user entity retrieved from the database.
        /// </param>
        /// <param name="request">
        /// The request containing updated user information.
        /// </param>
        /// <param name="isUserFound">
        /// Indicates whether the user exists in the system.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Boolean}"/> representing the result of the update operation.
        /// </returns>
        private async Task<ApiResponse<bool>> CreateResponse(User user, UpdateUserRequest request, bool isUserFound)
        {
            if (!isUserFound)
            {
                return ApiResponse<bool>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }
            // Map DTO data to entity and get updated user
            MapUserDataFromRequest(user, request);
            // Persist changes
            await _updateUser.Execute(user);
            return ApiResponse<bool>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), true
            );
        }

        /// <summary>
        /// Maps the DTO fields from the update request to the user entity.
        /// </summary>
        /// <param name="user">
        /// The user entity to be updated.
        /// </param>
        /// <param name="request">
        /// The request containing updated user information.
        /// </param>
        private void MapUserDataFromRequest(User user, UpdateUserRequest request)
        {
            user.Username = request.Username;
            user.FullName = request.FullName;
            user.DisplayName = request.DisplayName;
            user.AvatarUrl = request.AvatarUrl;
            user.Email = request.Email.ToLower();
            user.PhoneNumber = request.PhoneNumber;
            user.Role = request.Role;
            user.LastModifiedBy = "system";
            user.LastModifiedDate = DateTimeOffset.UtcNow;
            UpdateDeletionMetadata(user, request.IsDeleted);
        }

        /// <summary>
        /// Updates soft-delete metadata (DeletedAt, DeletedBy) based on the deletion state change.
        /// </summary>
        /// <param name="user">
        /// The user entity to update.
        /// </param>
        /// <param name="willBeDeleted">
        /// The target deletion state.
        /// </param>
        private void UpdateDeletionMetadata(User user, bool willBeDeleted)
        {
            var wasDeleted = user.IsDeleted;
            if (wasDeleted != willBeDeleted)
            {
                if (willBeDeleted)
                {
                    user.DeletedAt = DateTimeOffset.UtcNow;
                    user.DeletedBy = "system";
                }
                else
                {
                    user.DeletedAt = null;
                    user.DeletedBy = null;
                }
            }
            user.IsDeleted = willBeDeleted;
        }
    }
}