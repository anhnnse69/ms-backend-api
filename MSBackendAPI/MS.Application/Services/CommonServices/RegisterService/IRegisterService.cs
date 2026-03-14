using MS.Application.Common.Response;

namespace MS.Application.Services.CommonServices.RegisterService
{
    /// <summary>
    /// Register service interface
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Register process for Patient role
        /// </summary>
        Task<ApiResponse<RegisterResponse>> Process(RegisterRequest registerRequest);
    }
}