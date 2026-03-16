using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.CancelAppointmentService
{
    public class CancelAppointmentRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid Id { get; set; }
        public string Reason { get; set; }
    }
}
