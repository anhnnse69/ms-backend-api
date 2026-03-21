using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.DeleteFavoriteService
{
    /// <summary>
    /// Defines the contract for the delete favorite service.
    /// </summary>
    public interface IDeleteFavoriteService
    {
        /// <summary>
        /// Processes the request to soft-delete a favorite record.
        /// </summary>
        /// <param name="favoriteId">The unique identifier of the favorite record to delete.</param>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> indicating whether the deletion succeeded.
        /// </returns>
        Task<ApiResponse<DeleteFavoriteResponse>> Process(Guid favoriteId, Guid userId);
    }
}
