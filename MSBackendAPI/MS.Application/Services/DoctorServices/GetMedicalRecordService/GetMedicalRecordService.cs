using MS.Application.Common.Response;
using MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId;

namespace MS.Application.Services.DoctorServices.GetMedicalRecordService
{
    /// <summary>
    /// Service responsible for retrieving medical record by appointment
    /// </summary>
    public class GetMedicalRecordService : IGetMedicalRecordService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IGetMedicalRecordByAppointmentId _getMedicalRecord;

        public GetMedicalRecordService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IGetMedicalRecordByAppointmentId getMedicalRecord)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _getMedicalRecord = getMedicalRecord;
        }

        /// <summary>
        /// Process get medical record request
        /// </summary>
        public async Task<ApiResponse<UpdateDoctorMedicalRecordResponse>> Process(Guid userId, Guid appointmentId)
        {
            // 1. Init flags
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            bool isMedicalRecordValid = true;

            // 2. Retrieve data
            var doctor = await RetrieveDoctor(userId);
            var appointment = await RetrieveAppointment(appointmentId);
            var record = await RetrieveMedicalRecord(appointmentId);

            // 3. Validate
            ValidateDoctor(doctor, ref isDoctorValid);
            ValidateAppointment(appointment, ref isAppointmentValid);
            ValidateOwnership(doctor, appointment, ref isOwnershipValid);
            ValidateMedicalRecord(record, ref isMedicalRecordValid);

            // 4. Map response
            var response = MapToResponse(doctor, appointment, record);

            // 5. Return response
            return CreateResponse(
                response,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isMedicalRecordValid);
        }

        // ================== PRIVATE METHODS ==================

        private async Task<Doctor?> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        private async Task<Appointment?> RetrieveAppointment(Guid appointmentId)
        {
            return await _getAppointmentById.Execute(appointmentId);
        }

        private async Task<MedicalRecord?> RetrieveMedicalRecord(Guid appointmentId)
        {
            return await _getMedicalRecord.Execute(appointmentId);
        }

        private void ValidateDoctor(Doctor? doctor, ref bool isDoctorValid)
        {
            if (doctor == null)
            {
                isDoctorValid = false;
            }
        }

        private void ValidateAppointment(Appointment? appointment, ref bool isAppointmentValid)
        {
            if (appointment == null)
            {
                isAppointmentValid = false;
            }
        }

        private void ValidateOwnership(Doctor? doctor, Appointment? appointment, ref bool isOwnershipValid)
        {
            if (doctor != null && appointment != null)
            {
                if (appointment.DoctorId != doctor.Id)
                {
                    isOwnershipValid = false;
                }
            }
        }

        private void ValidateMedicalRecord(MedicalRecord? record, ref bool isMedicalRecordValid)
        {
            if (record == null)
            {
                isMedicalRecordValid = false;
            }
        }

        private UpdateDoctorMedicalRecordResponse? MapToResponse(
            Doctor? doctor,
            Appointment? appointment,
            MedicalRecord? record)
        {
            if (doctor == null || appointment == null || record == null)
            {
                return null;
            }

            return new UpdateDoctorMedicalRecordResponse
            {
                PatientName = appointment.Patient?.FullName ?? "",
                PatientDateOfBirth = appointment.Patient?.DateOfBirth.ToString("yyyy-MM-dd") ?? "",
                PatientGender = appointment.Patient?.Gender.ToString() ?? "",
                PatientPhoneNumber = appointment.Patient?.PhoneNumber,
                DoctorName = doctor.FullName,
                DoctorSpecialty = doctor.Specialty?.NameVi ?? "",
                AppointmentDate = appointment.AppointmentTime.ToString("yyyy-MM-dd"),
                FacilityName = appointment.Facility?.NameVi ?? "",
                AppointmentStatus = appointment.Status.ToString(),
                Symptoms = record.Symptoms ?? "",
                Diagnosis = record.Diagnosis ?? "",
                Notes = record.Notes
            };
        }

        private ApiResponse<UpdateDoctorMedicalRecordResponse> CreateResponse(
            UpdateDoctorMedicalRecordResponse? response,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isMedicalRecordValid)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }

            if (!isAppointmentValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }

            if (!isOwnershipValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }

            if (!isMedicalRecordValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4028.ToString());
            }

            return ApiResponse<UpdateDoctorMedicalRecordResponse>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), response!);
        }
    }
}