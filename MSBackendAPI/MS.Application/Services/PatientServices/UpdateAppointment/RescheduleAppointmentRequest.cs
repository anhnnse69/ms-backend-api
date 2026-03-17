using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.UpdateAppointment
{
    /// <summary>
    /// Request data for rescheduling an appointment
    /// </summary>
    public class RescheduleAppointmentRequest
    {
        /// <summary>
        /// ID of the appointment to reschedule - Received from Request Body
        /// </summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }

        /// <summary>
        /// New doctor ID
        /// </summary>
        public Guid? NewDoctorId { get; set; }

        /// <summary>
        /// New time for the appointment
        /// </summary>
        public DateTimeOffset? NewAppointmentTime { get; set; }
    }
}