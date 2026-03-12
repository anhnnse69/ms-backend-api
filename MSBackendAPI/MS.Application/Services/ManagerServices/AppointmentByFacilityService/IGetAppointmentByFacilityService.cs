using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.AppointmentByFacilityService
{
    public interface IGetAppointmentByFacilityService
    {
        Task<ApiResponse<List<AppointmentResponse>>> Process(AppointmentFilterRequest request);
    }
}
