using MS.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.Doctors.GetDoctorAppointmentService
{
    public interface IGetDoctorAppointmentService
    {
        Task<ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>> Process(Guid userId);
    }
}
