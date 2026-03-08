using MS.Application.Common.Response;

namespace MS.Application.Services.Doctors.GetDoctorAvailabilityService
{
    public interface IGetDoctorAvailabilityService
    {
        Task<ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>> Process(Guid userId, int page, int size);
    }
}
