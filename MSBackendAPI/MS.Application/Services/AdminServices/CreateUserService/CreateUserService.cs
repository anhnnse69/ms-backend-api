using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Repositories.PatientRepositories.CreateUser;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;

namespace MS.Application.Services.AdminServices.CreateUserService
{
    /// <summary>
    /// Provides an implementation of the ICreateUserService interface
    /// for handling user creation logic.
    /// </summary>
    public class CreateUserService : ICreateUserService
    {
        private readonly ICreateUser _createUser;
        private readonly IGetUserByEmail _getUserByEmail;

        /// <summary>
        /// Initializes a new instance of the CreateUserService class.
        /// </summary>
        /// <param name="createUser">
        /// Repository responsible for persisting new user data.
        /// </param>
        /// <param name="getUserByEmail">
        /// Repository used to retrieve existing users by email.
        /// </param>
        public CreateUserService( ICreateUser createUser, IGetUserByEmail getUserByEmail)
        {
            _createUser = createUser;
            _getUserByEmail = getUserByEmail;
        }

        /// <summary>
        /// Processes the create user request.
        /// </summary>
        /// <param name="request">
        /// The request containing the information needed to create a new user.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the result of the operation
        /// and the ID of the newly created user if successful.
        /// </returns>
        public async Task<ApiResponse<Guid>> Process(CreateUserRequest request)
        {
            bool isEmailAlreadyExists = false;
            var existingUser = await RetrieveUserData(request.Email.ToLower().Trim());
            ValidateUserData(existingUser, ref isEmailAlreadyExists);
            return await CreateResponse(request, isEmailAlreadyExists);
        }

        /// <summary>
        /// Retrieves an existing user by email.
        /// </summary>
        /// <param name="email">
        /// The email address used to search for an existing user.
        /// </param>
        /// <returns>
        /// A <see cref="User"/> entity if found; otherwise null.
        /// </returns>
        private async Task<User> RetrieveUserData(string email)
        {
            return await _getUserByEmail.Execute(email);
        }

        /// <summary>
        /// Validates whether the provided user already exists.
        /// </summary>
        /// <param name="existingUser">
        /// The existing user retrieved from the database.
        /// </param>
        /// <param name="isEmailAlreadyExists">
        /// A flag indicating whether the email already exists.
        /// </param>
        private void ValidateUserData(User existingUser, ref bool isEmailAlreadyExists)
        {
            if (existingUser != null)
            {
                isEmailAlreadyExists = true;
            }
        }

        /// <summary>
        /// Creates the final API response after validation.
        /// </summary>
        /// <param name="request">
        /// The create user request.
        /// </param>
        /// <param name="isEmailAlreadyExists">
        /// Indicates whether the email already exists in the system.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> representing the result of the operation.
        /// </returns>
        private async Task<ApiResponse<Guid>> CreateResponse(CreateUserRequest request, bool isEmailAlreadyExists)
        {
            if (isEmailAlreadyExists)
            {
                return ApiResponse<Guid>.Fail(
                    MessageCode.APP_MESSAGE_4017.ToString()
                );
            }
            var newUser = BuildUserEntity(request);
            await _createUser.Execute(newUser);
            return ApiResponse<Guid>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                newUser.Id
            );
        }

        /// <summary>
        /// Builds a new <see cref="User"/> entity from the request data.
        /// </summary>
        /// <param name="request">
        /// The create user request containing user information.
        /// </param>
        /// <returns>
        /// A newly constructed <see cref="User"/> entity ready to be persisted.
        /// </returns>
        private User BuildUserEntity(CreateUserRequest request)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email.ToLower(),
                Username = request.Email.ToLower(),
                FullName = request.FullName,
                DisplayName = request.FullName,
                AvatarUrl = request.AvatarUrl,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(request.Password),
                Role = request.Role,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false,
                CreateBy = "system",
                LastModifiedBy = "system",
                CreateDate = DateTime.UtcNow,
                LastModifiedDate = DateTimeOffset.UtcNow,
            };
        }
    }
}