using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.PatientServices.BookAppointmentService
{
    /// <summary>
    /// Represents the request payload for booking a medical appointment.
    /// </summary>
    public class BookAppointmentRequest
    {
        /// <summary>The unique identifier of the facility.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid FacilityId { get; set; }
        /// <summary>The unique identifier of the specialty.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid SpecialtyId { get; set; }
        /// <summary>The unique identifier of the doctor.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid DoctorId { get; set; }
        /// <summary>The desired appointment date and time.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public DateTimeOffset AppointmentTime { get; set; }
        /// <summary>Optional notes provided by the patient.</summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string? Notes { get; set; }
    }
}
