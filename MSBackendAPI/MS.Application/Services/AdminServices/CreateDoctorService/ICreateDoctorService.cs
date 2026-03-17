using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.CreateDoctorService
{
    /// <summary>
    /// Defines the contract for the create doctor service.
    /// </summary>
    public interface ICreateDoctorService
    {
        /// <summary>
        /// Processes the request to create a new doctor.
        /// </summary>
        /// <param name="request">
        /// The request containing the doctor information.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the ID of the newly created doctor.
        /// </returns>
        Task<ApiResponse<Guid>> Process(CreateDoctorRequest request);
    }
}
