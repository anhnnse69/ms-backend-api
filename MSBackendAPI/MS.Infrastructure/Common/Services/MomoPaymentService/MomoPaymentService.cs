using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MS.Infrastructure.Common.Services.MomoPaymentService
{
    /// <summary>
    /// Default implementation that calls MoMo's REST API using configuration values from appsettings.
    /// </summary>
    public class MomoPaymentService : IMomoPaymentService
    {
        private readonly IConfiguration _configuration;

        public MomoPaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MomoCreatePaymentResult> CreatePaymentAsync(Guid appointmentId, decimal amount, string orderInfo)
        {
            var momoSection = _configuration.GetSection("MoMo");
            var partnerCode = momoSection["PartnerCode"] ?? string.Empty;
            var accessKey = momoSection["AccessKey"] ?? string.Empty;
            var secretKey = momoSection["SecretKey"] ?? string.Empty;
            var payUrl = momoSection["PayUrl"] ?? string.Empty;
            var returnUrl = momoSection["ReturnUrl"] ?? string.Empty;
            var ipnUrl = momoSection["IpnUrl"] ?? string.Empty;

            if (string.IsNullOrWhiteSpace(partnerCode) || string.IsNullOrWhiteSpace(accessKey) ||
                string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(payUrl))
            {
                throw new InvalidOperationException("MoMo configuration is missing or incomplete.");
            }

            var orderId = $"{appointmentId:N}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            var requestId = Guid.NewGuid().ToString("N");
            var requestType = "captureWallet";
            var amountValue = Convert.ToInt64(Math.Round(amount));

            var extraDataRaw = $"appointmentId={appointmentId}";
            var extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(extraDataRaw));

            // Signature format from MoMo documentation
            var rawSignature =
                $"accessKey={accessKey}&amount={amountValue}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}" +
                $"&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={returnUrl}&requestId={requestId}&requestType={requestType}";

            var signature = SignHmacSha256(rawSignature, secretKey);

            var payload = new
            {
                partnerCode,
                partnerName = "MS Medical Scheduling",
                storeId = "MS_ONLINE",
                requestId,
                amount = amountValue.ToString(),
                orderId,
                orderInfo,
                redirectUrl = returnUrl,
                ipnUrl,
                lang = "vi",
                extraData,
                requestType,
                signature
            };

            using var client = new HttpClient();
            using var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(payUrl, content);
            httpResponse.EnsureSuccessStatusCode();
            var body = await httpResponse.Content.ReadAsStringAsync();

            var json = JsonSerializer.Deserialize<JsonElement>(body);

            var result = new MomoCreatePaymentResult
            {
                OrderId = json.TryGetProperty("orderId", out var orderIdProp) ? orderIdProp.GetString() ?? string.Empty : orderId,
                RequestId = json.TryGetProperty("requestId", out var requestIdProp) ? requestIdProp.GetString() ?? string.Empty : requestId,
                PayUrl = json.TryGetProperty("payUrl", out var payUrlProp) ? payUrlProp.GetString() ?? string.Empty : string.Empty,
                ResultCode = json.TryGetProperty("resultCode", out var resultCodeProp) ? resultCodeProp.GetInt32() : -1,
                Message = json.TryGetProperty("message", out var messageProp) ? messageProp.GetString() ?? string.Empty : string.Empty
            };

            if (result.ResultCode != 0 || string.IsNullOrWhiteSpace(result.PayUrl))
            {
                throw new InvalidOperationException($"MoMo create payment failed: {result.Message} (code {result.ResultCode}).");
            }

            return result;
        }

        public bool ValidateIpnSignature(MomoIpnRequest ipnRequest)
        {
            var momoSection = _configuration.GetSection("MoMo");
            var secretKey = momoSection["SecretKey"] ?? string.Empty;
            var accessKey = momoSection["AccessKey"] ?? string.Empty;

            if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(accessKey))
            {
                return false;
            }

            // Raw signature format from MoMo IPN documentation
            var rawSignature =
                $"accessKey={accessKey}&amount={ipnRequest.Amount}&extraData={ipnRequest.ExtraData}" +
                $"&message={ipnRequest.Message}&orderId={ipnRequest.OrderId}&orderInfo={ipnRequest.OrderInfo}" +
                $"&orderType={ipnRequest.OrderType}&partnerCode={ipnRequest.PartnerCode}&payType={ipnRequest.PayType}" +
                $"&requestId={ipnRequest.RequestId}&responseTime={ipnRequest.ResponseTime}&resultCode={ipnRequest.ResultCode}&transId={ipnRequest.TransId}";

            var expectedSignature = SignHmacSha256(rawSignature, secretKey);
            return string.Equals(expectedSignature, ipnRequest.Signature, StringComparison.OrdinalIgnoreCase);
        }

        private static string SignHmacSha256(string rawData, string secretKey)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
