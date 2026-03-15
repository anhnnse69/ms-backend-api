using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
    /// <summary>
    /// Interface for getting patient appointments service
    /// </summary>
    public interface IGetPatientAppointmentsService
    {
        Task<ApiResponse<GetPatientAppointmentsResponse>> Process(GetPatientAppointmentsRequest request);
    }
}
