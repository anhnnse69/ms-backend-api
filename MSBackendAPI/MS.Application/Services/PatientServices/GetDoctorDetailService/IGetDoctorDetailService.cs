using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Defines the contract for the get doctor detail service.
    /// </summary>
    public interface IGetDoctorDetailService
    {
        /// <summary>
        /// Processes the request to retrieve detailed profile information of a doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor to retrieve.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the doctor's full profile,
        /// specialties, facilities, and weekly availability schedule.
        /// </returns>
        Task<ApiResponse<GetDoctorDetailResponse>> Process(Guid doctorId);
    }
}
