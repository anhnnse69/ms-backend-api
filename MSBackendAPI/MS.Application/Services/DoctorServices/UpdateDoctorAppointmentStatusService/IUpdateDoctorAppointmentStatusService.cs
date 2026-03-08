using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    public interface IUpdateDoctorAppointmentStatusService
    {
        Task<ApiResponse<UpdateDoctorAppointmentStatusResponse>> Process(
            Guid userId,
            UpdateDoctorAppointmentStatusRequest request);
    }
}
