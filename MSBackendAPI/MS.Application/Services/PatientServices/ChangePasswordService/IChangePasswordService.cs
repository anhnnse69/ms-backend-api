using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.ChangePasswordService
{
    /// <summary>
    /// Contract for change password service
    /// </summary>
    public interface IChangePasswordService
    {
        /// <summary>
        /// Process change password request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse<ChangePasswordResponse>> Process(ChangePasswordRequest request);
    }
}