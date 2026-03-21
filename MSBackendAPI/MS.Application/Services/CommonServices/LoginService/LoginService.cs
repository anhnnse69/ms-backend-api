using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Common.Services.JwtService;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;

namespace MS.Application.Services.CommonServices.LoginService
{
    /// <summary>
    /// Login service implementation
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly IJwtTokenService _jwtTokenService;

        /// <summary>
        /// Login service constructor
        /// </summary>
        /// <param name="getUserByEmail"></param>
        public LoginService(IGetUserByEmail getUserByEmail, IJwtTokenService jwtTokenService)
        {
            _getUserByEmail = getUserByEmail;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Process login request
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<LoginResponse>> Proccess(LoginRequest loginRequest)
        {
            // 1. Initialize validation flags
            bool isRetrivedDataValid = true;
            bool isPasswordCorrect = true;
            // 2. Retrieve user by email address
            var retirvedUser = await RetrieveUserData(loginRequest.EmailAddress.ToLower());
            // 3. Validate retrieved data
            ValidateRetrivedData(retirvedUser, ref isRetrivedDataValid, ref isPasswordCorrect, loginRequest);
            // 4. Create response
            return await CreateResponse(retirvedUser, isRetrivedDataValid, isPasswordCorrect);
        }

        /// <summary>
        /// Create JWT token
        /// </summary>
        /// <param name="retirvedUser"></param>
        /// <returns></returns>
        private string CreateToken(User retirvedUser)
        {
            var token = _jwtTokenService.GenerateToken(retirvedUser);
            return token;
        }

        /// <summary>
        /// Create response of the login request
        /// </summary>
        /// <param name="isRetrieveDataValid"></param>
        /// <param name="isPasswordCorrect"></param>
        /// <returns></returns>
        private async Task<ApiResponse<LoginResponse>> CreateResponse(User retirvedUser, bool isRetrieveDataValid, bool isPasswordCorrect)
        {
            if (!isPasswordCorrect || !isRetrieveDataValid)
            {
                return ApiResponse<LoginResponse>.Fail(MessageCode.APP_MESSAGE_4016.ToString());
            }
            var token = CreateToken(retirvedUser);
            return ApiResponse<LoginResponse>.Success
            (
                MessageCode.APP_MESSAGE_2000.ToString(),
                new LoginResponse(token)
            );
        }

        /// <summary>
        /// Validate retrieved data
        /// </summary>
        /// <param name="retirvedData"></param>
        /// <param name="isRetrievedData"></param>
        /// <param name="loginRequest"></param>
        private void ValidateRetrivedData(User retirvedData, ref bool isRetrievedData, ref bool isPasswordCorrect, LoginRequest loginRequest)
        {
            if (retirvedData == null)
            {
                isRetrievedData = false;
            }
            if (retirvedData != null)
            {
                if (!PasswordHelper.VerifyPassword(loginRequest.Password, retirvedData.PasswordHash))
                {
                    isPasswordCorrect = false;
                }
            }
        }

        /// <summary>
        /// Retrieve user data by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private async Task<User> RetrieveUserData(string emailAddress)
        {
            return await _getUserByEmail.Execute(emailAddress);
        }
    }
}
