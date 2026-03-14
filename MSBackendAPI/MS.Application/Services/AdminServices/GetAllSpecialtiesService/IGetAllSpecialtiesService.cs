using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.GetAllSpecialtiesService
{
    /// <summary>
    /// Service interface for retrieving all specialties.
    /// </summary>
    public interface IGetAllSpecialtiesService
    {
        /// <summary>
        /// Processes request to retrieve paginated specialties.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>
        /// ApiResponse containing specialty list.
        /// </returns>
        Task<ApiResponse<IEnumerable<GetAllSpecialtiesResponse>>> Process(int page, int size);
    }
}