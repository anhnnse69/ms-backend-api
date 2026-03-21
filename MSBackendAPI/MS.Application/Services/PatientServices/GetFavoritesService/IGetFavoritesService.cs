using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.GetFavoritesService
{
    /// <summary>
    /// Defines the contract for the get favorites service.
    /// </summary>
    public interface IGetFavoritesService
    {
        /// <summary>
        /// Processes the request to retrieve the patient's paginated favorites list.
        /// </summary>
        /// <param name="request">The query parameters including page, size, and type filter.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the paginated list of favorites.
        /// </returns>
        Task<ApiResponse<List<GetFavoritesResponse>>> Process(GetFavoritesRequest request, Guid userId);
    }
}
