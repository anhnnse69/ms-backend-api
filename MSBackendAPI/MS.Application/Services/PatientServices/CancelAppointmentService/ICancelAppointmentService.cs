using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.CancelAppointmentService
{
    public interface ICancelAppointmentService
    {
        Task<ApiResponse<bool>> Proccess(CancelAppointmentRequest request);
    }
}
