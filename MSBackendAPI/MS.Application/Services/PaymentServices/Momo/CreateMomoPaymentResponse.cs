using System;

namespace MS.Application.Services.PaymentServices.Momo
{
    /// <summary>
    /// Response data returned to the client when a MoMo payment session is created.
    /// </summary>
    public class CreateMomoPaymentResponse
    {
        public Guid AppointmentId { get; set; }
        public string PayUrl { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
    }
}
