using MS.Domain.Enums.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.DoctorAppointmentService
{
    public class DoctorAppointmentResponse
    {
        public Guid AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string FacilityName { get; set; }

        public DateTimeOffset AppointmentTime { get; set; }
        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
