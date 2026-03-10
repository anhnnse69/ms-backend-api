using MS.Application.Services.ManagerServices.DoctorScheduleByFacilityService;

namespace MS.Application.Services.DoctorScheduleService
{
    /// <summary>
    /// Response model representing doctor schedule information
    /// </summary>
    public class DoctorScheduleResponse
    {
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }
        public List<ScheduleSlotResponse> Schedules { get; set; }

    }
}