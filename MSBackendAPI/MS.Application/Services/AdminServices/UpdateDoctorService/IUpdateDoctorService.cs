using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.UpdateDoctorService
{
    /// <summary>
    /// Defines the contract for handling doctor update operations.
    /// </summary>
    public interface IUpdateDoctorService
    {
        /// <summary>
        /// Processes the update doctor request.
        /// </summary>
        /// <param name="request">
        /// The request object containing updated doctor information.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the updated doctor ID.
        /// </returns>
        Task<ApiResponse<Guid>> Process(UpdateDoctorRequest request);
    }
}