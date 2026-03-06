using MS.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.GetDoctorAvailabilityService
{
    public interface IGetDoctorAvailabilityService
    {
        Task<ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>> Process(Guid userId);
    }
}
