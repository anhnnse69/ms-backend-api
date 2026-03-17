using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.SendNotificationService
{
    /// <summary>
    /// Request object for sending a notification about an appointment.
    /// </summary>
    public class SendNotificationRequest
    {
        /// <summary>Patient ID - extracted from JWT token at controller level.</summary>
        public Guid PatientId { get; set; }

        /// <summary>The ID of the appointment that the notification is about.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }

        /// <summary>Title of the notification in Vietnamese.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(255, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string TitleVi { get; set; } = string.Empty;

        /// <summary>Title of the notification in English.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(255, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string TitleEn { get; set; } = string.Empty;

        /// <summary>Content of the notification in Vietnamese.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(1000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string ContentVi { get; set; } = string.Empty;

        /// <summary>Content of the notification in English.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(1000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string ContentEn { get; set; } = string.Empty;

        /// <summary>Type of notification (e.g., AppointmentReminder, AppointmentConfirmed, etc.).</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public NotificationType Type { get; set; }

        /// <summary>Channel through which the notification will be sent (Email, SMS, InApp, Push).</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public NotificationChannel Channel { get; set; }
    }
}