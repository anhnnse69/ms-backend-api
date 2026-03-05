using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.DoctorAppointmentService
{
    /// <summary>
    /// Doctor appointment service implementation
    /// </summary>
    public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IGetDoctorAppointments _getDoctorAppointments;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        public DoctorAppointmentService(
            IGetDoctorAppointments getDoctorAppointments,
            IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAppointments = getDoctorAppointments;
            _getDoctorByUserId = getDoctorByUserId;
        }

        public async Task<ApiResponse<IEnumerable<DoctorAppointmentResponse>>> GetMyAppointments(Guid userId)
        {
            bool isValid = true;

            // 1️. Lấy Doctor từ UserId
            var doctor = await _getDoctorByUserId.Execute(userId);

            if (doctor == null)
            {
                return ApiResponse<IEnumerable<DoctorAppointmentResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            // 2️. Retrieve appointments bằng DoctorId
            var retrievedData = await RetrieveData(doctor.Id);

            // 3️. Validate
            ValidateData(retrievedData, ref isValid);

            // 4️. Create response
            return await CreateResponse(retrievedData, isValid);
        }

        private async Task<IEnumerable<Appointment>> RetrieveData(Guid doctorId)
        {
            return await _getDoctorAppointments.Execute(doctorId);
        }

        private void ValidateData(IEnumerable<Appointment> data, ref bool isValid)
        {
            if (data == null || !data.Any())
            {
                isValid = false;
            }
        }

        private async Task<ApiResponse<IEnumerable<DoctorAppointmentResponse>>> CreateResponse(
            IEnumerable<Appointment> data,
            bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<DoctorAppointmentResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }

            var result = data.Select(x => new DoctorAppointmentResponse
            {
                AppointmentId = x.Id,
                PatientName = x.Patient?.FullName,
                FacilityName = x.Facility?.NameVi,
                AppointmentTime = x.AppointmentTime,
                Reason = x.Reason,
                Status = x.Status
            });

            return ApiResponse<IEnumerable<DoctorAppointmentResponse>>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}