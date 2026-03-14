using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.FacilitySpecialtyService
{
    /// <summary>
    /// Interface for getting specialties available at a specific facility
    /// </summary>
    public interface IGetFacilitySpecialtiesService
    {
        Task<ApiResponse<List<FacilitySpecialtyResponse>>> Process(GetFacilitySpecialtiesRequest request);
    }
}
