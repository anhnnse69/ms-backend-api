using MS.Application.Common.Response;

namespace MS.Application.Services.LoginService
{
    /// <summary>
    /// Login service interface
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Login process
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<ApiResponse<LoginResponse>> Proccess(LoginRequest loginRequest);
    }
}
