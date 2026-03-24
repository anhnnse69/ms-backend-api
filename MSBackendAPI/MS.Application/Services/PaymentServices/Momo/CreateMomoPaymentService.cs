using System;
using System.Text;
using System.Threading.Tasks;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Common.Services.MomoPaymentService;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId;

namespace MS.Application.Services.PaymentServices.Momo
{
    /// <summary>
    /// Application-level service that orchestrates MoMo payment creation for appointment deposits.
    /// </summary>
    public class CreateMomoPaymentService : ICreateMomoPaymentService
    {
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;
        private readonly IGetPatientByUserId _getPatientByUserId;
        private readonly IMomoPaymentService _momoPaymentService;

        public CreateMomoPaymentService(
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment,
            IGetPatientByUserId getPatientByUserId,
            IMomoPaymentService momoPaymentService)
        {
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
            _getPatientByUserId = getPatientByUserId;
            _momoPaymentService = momoPaymentService;
        }

        public async Task<ApiResponse<CreateMomoPaymentResponse>> Process(CreateMomoPaymentRequest request, Guid userId)
        {
            var patient = await _getPatientByUserId.Execute(userId);
            if (patient == null)
            {
                return ApiResponse<CreateMomoPaymentResponse>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            }
            var appointment = await _getAppointmentById.Execute(request.AppointmentId);
            if (appointment == null)
            {
                return ApiResponse<CreateMomoPaymentResponse>.Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }
            if (appointment.PatientId != patient.Id)
            {
                // Not owner of this appointment
                return ApiResponse<CreateMomoPaymentResponse>.Fail(MessageCode.APP_MESSAGE_4001.ToString());
            }
            if (appointment.IsDepositPaid)
            {
                // Deposit already paid
                return ApiResponse<CreateMomoPaymentResponse>.Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            var amount = appointment.DepositAmount ?? 0m;
            if (amount <= 0)
            {
                // No deposit required
                return ApiResponse<CreateMomoPaymentResponse>.Fail(MessageCode.APP_MESSAGE_4015.ToString());
            }
            var orderInfo = !string.IsNullOrWhiteSpace(request.OrderInfo)
                ? request.OrderInfo!
                : $"Deposit for appointment {appointment.Id}";
            var momoResult = await _momoPaymentService.CreatePaymentAsync(appointment.Id, amount, orderInfo);
            appointment.PaymentMethod = "MoMo";
            appointment.PaymentTransactionId = momoResult.OrderId;
            await _updateAppointment.Execute(appointment);
            var response = new CreateMomoPaymentResponse
            {
                AppointmentId = appointment.Id,
                PayUrl = momoResult.PayUrl,
                OrderId = momoResult.OrderId,
                RequestId = momoResult.RequestId
            };
            return ApiResponse<CreateMomoPaymentResponse>.Success(MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
