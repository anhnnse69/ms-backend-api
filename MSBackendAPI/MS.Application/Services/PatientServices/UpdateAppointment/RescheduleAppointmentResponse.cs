using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.PatientServices.UpdateAppointment
{
    /// <summary>
    /// Response data after rescheduling
    /// </summary>
    public class RescheduleAppointmentResponse
    {
        public Guid AppointmentId { get; set; }
        public DateTimeOffset UpdatedAppointmentTime { get; set; }
        public Guid? UpdatedDoctorId { get; set; }
    }
}
