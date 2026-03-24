using System;
using System.Threading.Tasks;
using MS.Application.Common.Response;

namespace MS.Application.Services.PaymentServices.Momo
{
    public interface ICreateMomoPaymentService
    {
        Task<ApiResponse<CreateMomoPaymentResponse>> Process(CreateMomoPaymentRequest request, Guid userId);
    }
}
