using System;
using System.Threading.Tasks;

namespace MS.Infrastructure.Common.Services.MomoPaymentService
{
    /// <summary>
    /// Defines operations for interacting with the MoMo payment gateway.
    /// </summary>
    public interface IMomoPaymentService
    {
        /// <summary>
        /// Creates a MoMo payment for the specified appointment and amount and returns the redirect URL.
        /// </summary>
        /// <param name="appointmentId">Associated appointment identifier.</param>
        /// <param name="amount">Deposit amount to be charged.</param>
        /// <param name="orderInfo">Human-readable description for the order.</param>
        /// <returns>Result containing MoMo pay URL and generated order identifiers.</returns>
        Task<MomoCreatePaymentResult> CreatePaymentAsync(Guid appointmentId, decimal amount, string orderInfo);

        /// <summary>
        /// Validates the IPN payload signature received from MoMo.
        /// </summary>
        /// <param name="ipnRequest">Raw IPN request object.</param>
        /// <returns>True if signature is valid; otherwise false.</returns>
        bool ValidateIpnSignature(MomoIpnRequest ipnRequest);
    }
}
