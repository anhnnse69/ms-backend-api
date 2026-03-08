using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;
using MS.Infrastructure.Repositories.PatientRepositories.UpdatePassword;

namespace MS.Application.Services.PatientServices.ChangePasswordService
{
    /// <summary>
    /// Implementation for change password service
    /// </summary>
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IGetUserById _getUserById;
        private readonly IUpdatePassword _updatePassword;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="getUserById"></param>
        public ChangePasswordService(IGetUserById getUserById, IUpdatePassword updateUserPassword)
        {
            _getUserById = getUserById;
            _updatePassword = updateUserPassword;
        }

        /// <summary>
        /// Process change password request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ChangePasswordResponse>> Process(ChangePasswordRequest request)
        {
            // Step 1: Initialize validation flags
            bool isUserExist = true;
            bool isCurrentPasswordCorrect = true;
            // Step 2: Retrieve user data
            var retrievedUser = await RetrieveUserData(request.UserId);
            // Step 3: Validate retrieved data
            ValidateRetrievedData(retrievedUser, request, ref isUserExist, ref isCurrentPasswordCorrect);
            // Step 4: Update password
            await UpdatePassword(retrievedUser, request.NewPassword, isUserExist, isCurrentPasswordCorrect);
            // Step 5: Create response
            return await CreateResponse(isUserExist, isCurrentPasswordCorrect);
        }

        /// <summary>
        /// Retrieve user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<User> RetrieveUserData(Guid userId)
        {
            return await _getUserById.Execute(userId);
        }

        /// <summary>
        /// Validate retrieved user and password
        /// </summary>
        /// <param name="retrievedUser"></param>
        /// <param name="request"></param>
        /// <param name="isUserExist"></param>
        /// <param name="isCurrentPasswordCorrect"></param>
        private void ValidateRetrievedData(
            User retrievedUser,
            ChangePasswordRequest request,
            ref bool isUserExist,
            ref bool isCurrentPasswordCorrect)
        {
            if (retrievedUser == null)
            {
                isUserExist = false;
            }
            if (retrievedUser != null)
            {
                if (!PasswordHelper.VerifyPassword(request.CurrentPassword, retrievedUser.PasswordHash))
                {
                    isCurrentPasswordCorrect = false;
                }
            }
        }

        /// <summary>
        /// Update password in database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <param name="isUserExist"></param>
        /// <param name="isCurrentPasswordCorrect"></param>
        /// <returns></returns>
        private async Task UpdatePassword(
    User user,
    string newPassword,
    bool isUserExist,
    bool isCurrentPasswordCorrect)
        {
            if (isUserExist && isCurrentPasswordCorrect)
            {
                user.PasswordHash = PasswordHelper.HashPassword(newPassword);
                await _updatePassword.Execute(user);
            }
        }

        /// <summary>
        /// Create response
        /// </summary>
        /// <param name="isUserExist"></param>
        /// <param name="isCurrentPasswordCorrect"></param>
        /// <returns></returns>
        private async Task<ApiResponse<ChangePasswordResponse>> CreateResponse(
            bool isUserExist,
            bool isCurrentPasswordCorrect)
        {
            if (!isUserExist)
            {
                return ApiResponse<ChangePasswordResponse>.Fail(
                    MessageCode.APP_MESSAGE_4010.ToString());
            }
            if (!isCurrentPasswordCorrect)
            {
                return ApiResponse<ChangePasswordResponse>.Fail(
                    MessageCode.APP_MESSAGE_4016.ToString());
            }
            return ApiResponse<ChangePasswordResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new ChangePasswordResponse("Password changed successfully"));
        }
    }
}