using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.CreateSpecialtyService
{
    /// <summary>
    /// Defines the contract for the create specialty service.
    /// </summary>
    public interface ICreateSpecialtyService
    {
        /// <summary>
        /// Processes the request to create a new specialty.
        /// </summary>
        /// <param name="request">
        /// The request containing the specialty information required for creation.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the ID of the newly created specialty.
        /// </returns>
        Task<ApiResponse<Guid>> Process(CreateSpecialtyRequest request);
    }
}