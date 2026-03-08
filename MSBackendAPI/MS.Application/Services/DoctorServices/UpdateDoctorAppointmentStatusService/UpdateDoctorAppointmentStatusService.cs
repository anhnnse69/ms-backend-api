using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Service responsible for updating doctor appointment status
    /// </summary>
    public class UpdateDoctorAppointmentStatusService : IUpdateDoctorAppointmentStatusService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;

        public UpdateDoctorAppointmentStatusService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
        }

        public async Task<ApiResponse<UpdateDoctorAppointmentStatusResponse>> Process(
            Guid userId,
            UpdateDoctorAppointmentStatusRequest request)
        {
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            bool isStatusTransitionValid = true;

            var retrievedDoctor = await RetrieveDoctor(userId);
            var retrievedAppointment = await RetrieveAppointment(request.AppointmentId);

            ValidateDoctor(retrievedDoctor, ref isDoctorValid);
            ValidateAppointment(retrievedAppointment, ref isAppointmentValid);
            ValidateOwnership(retrievedDoctor, retrievedAppointment, ref isOwnershipValid);
            ValidateStatusTransition(retrievedAppointment, request.Status, ref isStatusTransitionValid);

            await UpdateAppointmentStatus(retrievedAppointment, request.Status, isStatusTransitionValid);

            return CreateResponse(
                retrievedAppointment,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusTransitionValid);
        }

        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        private async Task<Appointment> RetrieveAppointment(Guid appointmentId)
        {
            return await _getAppointmentById.Execute(appointmentId);
        }

        private void ValidateDoctor(Doctor doctor, ref bool isDoctorValid)
        {
            if (doctor == null)
            {
                isDoctorValid = false;
            }
        }

        private void ValidateAppointment(Appointment appointment, ref bool isAppointmentValid)
        {
            if (appointment == null)
            {
                isAppointmentValid = false;
            }
        }

        private void ValidateOwnership(
            Doctor doctor,
            Appointment appointment,
            ref bool isOwnershipValid)
        {
            if (doctor != null && appointment != null)
            {
                if (appointment.DoctorId != doctor.Id)
                {
                    isOwnershipValid = false;
                }
            }
        }

        /// <summary>
        /// Validate status transition according to business flow
        /// </summary>
        private void ValidateStatusTransition(
            Appointment appointment,
            AppointmentStatus newStatus,
            ref bool isStatusTransitionValid)
        {
            if (appointment == null)
            {
                return;
            }

            var currentStatus = appointment.Status;

            isStatusTransitionValid = currentStatus switch
            {
                AppointmentStatus.PendingConfirmation =>
                    newStatus == AppointmentStatus.Confirmed ||
                    newStatus == AppointmentStatus.Cancelled,

                AppointmentStatus.Confirmed =>
                    newStatus == AppointmentStatus.CheckedIn ||
                    newStatus == AppointmentStatus.Cancelled ||
                    newStatus == AppointmentStatus.NoShow,

                AppointmentStatus.CheckedIn =>
                    newStatus == AppointmentStatus.InProgress,

                AppointmentStatus.InProgress =>
                    newStatus == AppointmentStatus.Completed,

                _ => false
            };
        }

        private async Task UpdateAppointmentStatus(
            Appointment appointment,
            AppointmentStatus status,
            bool isStatusTransitionValid)
        {
            if (appointment != null && isStatusTransitionValid)
            {
                appointment.Status = status;
                await _updateAppointment.Execute(appointment);
            }
        }

        private UpdateDoctorAppointmentStatusResponse MapToResponse(Appointment appointment)
        {
            return new UpdateDoctorAppointmentStatusResponse
            {
                AppointmentId = appointment.Id,
                Status = appointment.Status
            };
        }

        private ApiResponse<UpdateDoctorAppointmentStatusResponse> CreateResponse(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusTransitionValid)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }

            if (!isAppointmentValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }

            if (!isOwnershipValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }

            if (!isStatusTransitionValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4013.ToString());
            }

            var result = MapToResponse(appointment);

            return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                .Success(MessageCode.APP_MESSAGE_2007.ToString(), result);
        }
    }
}