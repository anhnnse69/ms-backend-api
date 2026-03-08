using MS.Application.Common.Response;

namespace MS.Application.Services.Doctors.GetDoctorAppointmentService
{
    public interface IGetDoctorAppointmentService
    {
        Task<ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>> Process(Guid userId, int page, int size);
    }
}
