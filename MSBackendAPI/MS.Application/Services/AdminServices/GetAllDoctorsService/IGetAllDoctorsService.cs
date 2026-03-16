using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.GetAllDoctorsService
{
    /// <summary>
    /// Service interface for retrieving all doctors.
    /// </summary>
    public interface IGetAllDoctorsService
    {
        /// <summary>
        /// Process request to get paginated doctor list.
        /// </summary>
        /// <param name="page">Current page index.</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>Paginated list of doctors.</returns>
        Task<ApiResponse<IEnumerable<GetAllDoctorsResponse>>> Process(int page, int size);
    }
}
