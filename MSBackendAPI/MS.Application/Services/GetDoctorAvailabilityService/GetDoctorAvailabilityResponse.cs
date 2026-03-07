using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.GetDoctorAvailabilityService
{
    /// <summary>
    /// Response model for doctor availability
    /// </summary>
    public class GetDoctorAvailabilityResponse
    {
        // Availability identifier
        public Guid Id { get; set; }

        // Facility identifier where the doctor works
        public Guid FacilityId { get; set; }

        // Facility name
        public string FacilityName { get; set; }

        // Day of week when doctor is available
        public DayOfWeek DayOfWeek { get; set; }

        // Start time of the availability slot
        public TimeSpan StartTime { get; set; }

        // End time of the availability slot
        public TimeSpan EndTime { get; set; }

        // Duration of each appointment slot in minutes
        public int SlotDurationMinutes { get; set; }

        // Indicates whether the availability is active
        public bool IsActive { get; set; }
    }
}