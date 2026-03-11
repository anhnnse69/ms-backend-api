using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Request model for updating appointment status.
    /// </summary>
    public class UpdateDoctorAppointmentStatusRequest
    {
        /// <summary>Appointment identifier.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }

        /// <summary>New appointment status.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = nameof(MessageCode.APP_MESSAGE_4013))]
        public AppointmentStatus Status { get; set; }
    }
}