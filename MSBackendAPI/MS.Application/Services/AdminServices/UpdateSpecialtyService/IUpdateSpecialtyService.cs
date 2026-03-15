using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.UpdateSpecialtyService
{
    /// <summary>
    /// Defines the contract for updating specialty information.
    /// </summary>
    public interface IUpdateSpecialtyService
    {
        /// <summary>
        /// Processes the request to update a specialty.
        /// </summary>
        Task<ApiResponse<bool>> Process(Guid id, UpdateSpecialtyRequest request);
    }
}