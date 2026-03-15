using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
    /// <summary>
    /// DTO representing appointment data for UI
    /// </summary>
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string DoctorName { get; set; }
        public string FacilityName { get; set; }
        public string SpecialtyName { get; set; }
    }
}
