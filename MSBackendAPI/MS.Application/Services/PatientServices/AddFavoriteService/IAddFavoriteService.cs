using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.AddFavoriteService
{
    /// <summary>
    /// Defines the contract for the add favorite service.
    /// </summary>
    public interface IAddFavoriteService
    {
        /// <summary>
        /// Processes the request to add a doctor or facility to the patient's favorites.
        /// </summary>
        /// <param name="request">The request containing the doctorId or facilityId to favorite.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created or restored favorite record.
        /// </returns>
        Task<ApiResponse<AddFavoriteResponse>> Process(AddFavoriteRequest request, Guid userId);
    }
}
