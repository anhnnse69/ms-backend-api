using MS.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.DoctorAppointmentService
{
    public interface IDoctorAppointmentService
    {
        Task<ApiResponse<IEnumerable<DoctorAppointmentResponse>>> GetMyAppointments(Guid userId);
    }
}
