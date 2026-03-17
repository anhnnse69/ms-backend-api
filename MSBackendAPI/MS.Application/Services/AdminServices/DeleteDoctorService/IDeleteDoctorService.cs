using MS.Application.Common.Response;

namespace MS.Application.Services.AdminServices.DeleteDoctorService
{
    /// <summary>
    /// Interface for delete doctor service.
    /// </summary>
    public interface IDeleteDoctorService
    {
        Task<ApiResponse<DeleteDoctorResponse>> Process(Guid id);
    }
}
