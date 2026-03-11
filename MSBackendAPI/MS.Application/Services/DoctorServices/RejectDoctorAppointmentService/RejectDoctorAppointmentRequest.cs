using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.DoctorServices.RejectDoctorAppointmentService
{
    /// <summary>
    /// Request model for rejecting doctor appointment.
    /// </summary>
    public class RejectDoctorAppointmentRequest
    {
        /// <summary>Appointment identifier.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }

        /// <summary>Reason for rejecting the appointment.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(500, MinimumLength = 10, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Reason { get; set; } = string.Empty;
    }
}