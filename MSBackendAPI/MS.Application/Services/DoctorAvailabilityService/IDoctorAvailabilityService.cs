using MS.Application.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.DoctorAvailabilityService
{
    public interface IDoctorAvailabilityService
    {
        Task<ApiResponse<IEnumerable<DoctorAvailabilityResponse>>> GetMyAvailabilities(Guid userId);
    }
}
