using System;
using System.ComponentModel.DataAnnotations;
using MS.Domain.Enums.GeneralCodes;

namespace MS.Application.Services.PaymentServices.Momo
{
    /// <summary>
    /// Request payload to initiate a MoMo payment for an existing appointment.
    /// </summary>
    public class CreateMomoPaymentRequest
    {
        /// <summary>The appointment identifier that the deposit is associated with.</summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid AppointmentId { get; set; }

        /// <summary>Optional description to show in MoMo order info.</summary>
        [MaxLength(250, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public string? OrderInfo { get; set; }
    }
}
