using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Roles;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Repositories.PatientRepositories.CreateNewAccount;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;

namespace MS.Application.Services.CommonServices.RegisterService
{
    /// <summary>
    /// Register service implementation for Patient role
    /// </summary>
    public class RegisterService : IRegisterService
    {
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly ICreateAccount _createAccount;

        /// <summary>
        /// Register service constructor
        /// </summary>
        /// <param name="getUserByEmail"></param>
        /// <param name="createUser"></param>
        public RegisterService(
            IGetUserByEmail getUserByEmail,
            ICreateAccount createAccount)
        {
            _getUserByEmail = getUserByEmail;
            _createAccount = createAccount;
        }

        /// <summary>
        /// Process register request
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<RegisterResponse>> Process(RegisterRequest registerRequest)
        {
            // 1. Initialize validation flags
            bool isEmailAvailable = true;
            // 2. Retrieve existing user by email
            var retrievedUser = await RetrieveUserData(registerRequest.EmailAddress.ToLower());
            // 3. Validate retrieved data
            ValidateRetrievedData(retrievedUser, ref isEmailAvailable);
            // 4. Create response
            return await CreateResponse(registerRequest, isEmailAvailable);
        }

        /// <summary>
        /// Retrieve user data by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private async Task<User?> RetrieveUserData(string emailAddress)
        {
            return await _getUserByEmail.Execute(emailAddress);
        }

        /// <summary>
        /// Validate whether email is already registered
        /// </summary>
        /// <param name="retrievedUser"></param>
        /// <param name="isEmailAvailable"></param>
        private void ValidateRetrievedData(User? retrievedUser, ref bool isEmailAvailable)
        {
            if (retrievedUser != null)
            {
                isEmailAvailable = false;
            }
        }

        /// <summary>
        /// Persist new User entity with embedded Patient via navigation property.
        /// Reuses ICreateUser — EF Core inserts both User and Patient in one round-trip.
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        private async Task<User> PersistUser(RegisterRequest registerRequest)
        {
            var newUser = BuildUserEntity(registerRequest);
            return await _createAccount.Execute(newUser);
        }

        /// <summary>
        /// Create response of the register request
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <param name="isEmailAvailable"></param>
        /// <returns></returns>
        private async Task<ApiResponse<RegisterResponse>> CreateResponse(
            RegisterRequest registerRequest,
            bool isEmailAvailable)
        {
            if (!isEmailAvailable)
            {
                return ApiResponse<RegisterResponse>.Fail(MessageCode.APP_MESSAGE_4017.ToString());
            }
            var createdUser = await PersistUser(registerRequest);
            return ApiResponse<RegisterResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new RegisterResponse(createdUser.Email, createdUser.FullName)
            );
        }

        /// <summary>
        /// Build User entity with embedded Patient navigation property.
        /// EF Core will insert both records in one SaveChanges call via ICreateUser.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private User BuildUserEntity(RegisterRequest request)
        {
            var normalizedEmail = request.EmailAddress.ToLower();

            return new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                DisplayName = request.FullName,
                Email = normalizedEmail,
                Username = normalizedEmail,
                PasswordHash = PasswordHelper.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                Role = SystemRole.Patient,
                CreateBy = normalizedEmail,
                LastModifiedBy = normalizedEmail,
                Patient = BuildPatientEntity(request, normalizedEmail)
            };
        }

        /// <summary>
        /// Build Patient entity linked via User.Patient navigation property
        /// </summary>
        /// <param name="request"></param>
        /// <param name="normalizedEmail"></param>
        /// <returns></returns>
        private Patient BuildPatientEntity(RegisterRequest request, string normalizedEmail)
        {
            return new Patient
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                DisplayName = request.FullName,
                Email = normalizedEmail,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                CreateBy = normalizedEmail,
                LastModifiedBy = normalizedEmail
            };
        }
    }
}