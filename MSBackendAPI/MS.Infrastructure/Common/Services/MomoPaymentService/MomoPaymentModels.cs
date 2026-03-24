using System;

namespace MS.Infrastructure.Common.Services.MomoPaymentService
{
    /// <summary>
    /// Strongly typed representation of the response returned by MoMo when creating a payment.
    /// Only the fields required by this system are modeled.
    /// </summary>
    public class MomoCreatePaymentResult
    {
        public string OrderId { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public string PayUrl { get; set; } = string.Empty;
        public int ResultCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representation of the IPN callback payload sent by MoMo.
    /// This matches the official MoMo documentation for captureWallet.
    /// </summary>
    public class MomoIpnRequest
    {
        public string PartnerCode { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public long Amount { get; set; }
        public long TransId { get; set; }
        public string OrderInfo { get; set; } = string.Empty;
        public string OrderType { get; set; } = string.Empty;
        public long ResponseTime { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string PayType { get; set; } = string.Empty;
        public string ExtraData { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
    }
}
