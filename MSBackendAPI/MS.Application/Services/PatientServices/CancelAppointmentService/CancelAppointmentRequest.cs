using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.CancelAppointmentService
{
    public class CancelAppointmentRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid Id { get; set; }

        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        public string Reason { get; set; }
    }
}
