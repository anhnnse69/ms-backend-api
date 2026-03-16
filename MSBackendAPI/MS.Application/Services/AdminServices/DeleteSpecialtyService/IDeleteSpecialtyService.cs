using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.DeleteSpecialtyService
{
    /// <summary>
    /// Delete specialty service interface
    /// </summary>
    public interface IDeleteSpecialtyService
    {
        /// <summary>
        /// Process delete specialty request
        /// </summary>
        /// <param name="id">The unique identifier of the specialty.</param>
        /// <returns></returns>
        Task<ApiResponse<DeleteSpecialtyResponse>> Process(Guid id);
    }
}