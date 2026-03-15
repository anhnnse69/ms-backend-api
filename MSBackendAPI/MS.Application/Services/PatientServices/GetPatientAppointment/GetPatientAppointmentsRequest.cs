using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
    /// <summary>
    /// Request model for getting patient appointments
    /// </summary>
    public class GetPatientAppointmentsRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid PatientId { get; set; }
    }
}
