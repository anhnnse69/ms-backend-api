using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateMedicalRecord;

namespace MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService
{
    /// <summary>
    /// Service responsible for updating medical record for a doctor appointment
    /// </summary>
    public class UpdateDoctorMedicalRecordService : IUpdateDoctorMedicalRecordService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IGetMedicalRecordByAppointmentId _getMedicalRecordByAppointmentId;
        private readonly IUpdateMedicalRecord _updateMedicalRecord;

        /// <summary>
        /// Constructor for UpdateDoctorMedicalRecordService
        /// </summary>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        /// <param name="getAppointmentById">Repository to retrieve appointment by id</param>
        /// <param name="getMedicalRecordByAppointmentId">Repository to retrieve medical record by appointment id</param>
        /// <param name="updateMedicalRecord">Repository to update medical record</param>
        public UpdateDoctorMedicalRecordService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IGetMedicalRecordByAppointmentId getMedicalRecordByAppointmentId,
            IUpdateMedicalRecord updateMedicalRecord)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _getMedicalRecordByAppointmentId = getMedicalRecordByAppointmentId;
            _updateMedicalRecord = updateMedicalRecord;
        }

        /// <summary>
        /// Process update medical record request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Update medical record request</param>
        /// <returns>API response containing updated medical record information</returns>
        public async Task<ApiResponse<UpdateDoctorMedicalRecordResponse>> Process(Guid userId, Guid appointmentId, UpdateDoctorMedicalRecordRequest request)
        {
            // 1. Initialize validation flags
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            bool isStatusValid = true;
            bool isMedicalRecordValid = true;
            // 2. Retrieve doctor entity
            var doctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment entity
            var appointment = await RetrieveAppointment(appointmentId);
            // 4. Retrieve medical record entity
            var medicalRecord = await RetrieveMedicalRecord(appointmentId);
            // 5. Validate retrieved data
            ValidateDoctor(doctor, ref isDoctorValid);
            ValidateAppointment(appointment, ref isAppointmentValid);
            ValidateOwnership(doctor, appointment, ref isOwnershipValid);
            ValidateStatus(appointment, ref isStatusValid);
            ValidateMedicalRecord(medicalRecord, ref isMedicalRecordValid);
            // 6. Update medical record
            await UpdateMedicalRecord(
                doctor,
                medicalRecord,
                request,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusValid,
                isMedicalRecordValid);
            // 7. Map response
            var response = MapToResponse(doctor, appointment, medicalRecord);
            // 8. Create API response
            return CreateResponse(
                response,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusValid,
                isMedicalRecordValid);
        }

        /// <summary>
        /// Retrieve doctor by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor?> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve appointment entity
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Appointment entity</returns>
        private async Task<Appointment?> RetrieveAppointment(Guid appointmentId)
        {
            return await _getAppointmentById.Execute(appointmentId);
        }

        /// <summary>
        /// Retrieve medical record by appointment identifier
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Medical record entity</returns>
        private async Task<MedicalRecord?> RetrieveMedicalRecord(Guid appointmentId)
        {
            return await _getMedicalRecordByAppointmentId.Execute(appointmentId);
        }

        /// <summary>
        /// Validate doctor existence
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="isDoctorValid">Validation flag</param>
        private void ValidateDoctor(Doctor? doctor, ref bool isDoctorValid)
        {
            if (doctor == null)
            {
                isDoctorValid = false;
            }
        }

        /// <summary>
        /// Validate appointment existence
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isAppointmentValid">Validation flag</param>
        private void ValidateAppointment(Appointment? appointment, ref bool isAppointmentValid)
        {
            if (appointment == null)
            {
                isAppointmentValid = false;
            }
        }

        /// <summary>
        /// Validate doctor ownership of the appointment
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isOwnershipValid">Validation flag</param>
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

        /// <summary>
        /// Validate appointment status before updating medical record
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isStatusValid">Validation flag</param>
        private void ValidateStatus(Appointment? appointment, ref bool isStatusValid)
        {
            if (appointment != null)
            {
                if (appointment.Status != AppointmentStatus.Completed)
                {
                    isStatusValid = false;
                }
            }
        }

        /// <summary>
        /// Validate medical record existence
        /// </summary>
        /// <param name="record">Medical record entity</param>
        /// <param name="isMedicalRecordValid">Validation flag</param>
        private void ValidateMedicalRecord(MedicalRecord? record, ref bool isMedicalRecordValid)
        {
            if (record == null)
            {
                isMedicalRecordValid = false;
            }
        }

        /// <summary>
        /// Update medical record entity
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="medicalRecord">Medical record entity</param>
        /// <param name="request">Update medical record request</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        /// <param name="isStatusValid">Appointment status validation flag</param>
        /// <param name="isMedicalRecordValid">Medical record validation flag</param>
        /// <returns>Task representing the update operation</returns>
        private async Task UpdateMedicalRecord(
            Doctor doctor,
            MedicalRecord medicalRecord,
            UpdateDoctorMedicalRecordRequest request,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusValid,
            bool isMedicalRecordValid)
        {
            if (isDoctorValid &&
                isAppointmentValid &&
                isOwnershipValid &&
                isStatusValid &&
                isMedicalRecordValid &&
                medicalRecord != null &&
                doctor != null)
            {
                medicalRecord.Symptoms = request.Symptoms;
                medicalRecord.Diagnosis = request.Diagnosis;
                medicalRecord.Notes = request.Notes;
                medicalRecord.LastModifiedBy = doctor.Id.ToString();
                await _updateMedicalRecord.Execute(medicalRecord);
            }
        }

        /// <summary>
        /// Map entities to response model
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="medicalRecord">Medical record entity</param>
        /// <returns>Update medical record response</returns>
        private UpdateDoctorMedicalRecordResponse? MapToResponse(Doctor doctor, Appointment appointment, MedicalRecord medicalRecord)
        {
            if (doctor == null || appointment == null || medicalRecord == null)
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
                Symptoms = medicalRecord.Symptoms ?? "",
                Diagnosis = medicalRecord.Diagnosis ?? "",
                Notes = medicalRecord.Notes
            };
        }

        /// <summary>
        /// Create API response
        /// </summary>
        /// <param name="response">Mapped response model</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        /// <param name="isStatusValid">Appointment status validation flag</param>
        /// <param name="isMedicalRecordValid">Medical record validation flag</param>
        /// <returns>API response</returns>
        private ApiResponse<UpdateDoctorMedicalRecordResponse> CreateResponse(
            UpdateDoctorMedicalRecordResponse response,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusValid,
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
            if (!isStatusValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4013.ToString());
            }
            if (!isMedicalRecordValid)
            {
                return ApiResponse<UpdateDoctorMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4028.ToString());
            }
            return ApiResponse<UpdateDoctorMedicalRecordResponse>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
