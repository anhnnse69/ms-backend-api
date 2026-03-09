using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorDetailByFacilityService
{
    /// <summary>
    /// Interface for retrieving doctor detail information
    /// </summary>
    public interface IDoctorDetailByFacilityService
    {
        /// <summary>
        /// Process request to retrieve doctor detail by identifier
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Doctor detail response</returns>
        Task<ApiResponse<DoctorDetailResponse>> Process(Guid doctorId);
    }
}