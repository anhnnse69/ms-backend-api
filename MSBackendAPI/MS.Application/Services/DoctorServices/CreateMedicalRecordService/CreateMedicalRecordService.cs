using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.CreateMedicalRecord;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.Application.Services.DoctorServices.CreateMedicalRecordService
{
    /// <summary>
    /// Service responsible for creating medical record
    /// </summary>
    public class CreateMedicalRecordService : ICreateMedicalRecordService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IGetMedicalRecordByAppointmentId _getMedicalRecordByAppointmentId;
        private readonly ICreateMedicalRecord _createMedicalRecord;
        private readonly IUpdateAppointment _updateAppointment;

        /// <summary>
        /// Constructor for CreateMedicalRecordService
        /// </summary>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        /// <param name="getAppointmentById">Repository to retrieve appointment by id</param>
        /// <param name="getMedicalRecordByAppointmentId">Repository to retrieve medical record by appointment id</param>
        /// <param name="createMedicalRecord">Repository to create medical record</param>
        /// <param name="updateAppointment">Repository to update appointment</param>
        public CreateMedicalRecordService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IGetMedicalRecordByAppointmentId getMedicalRecordByAppointmentId,
            ICreateMedicalRecord createMedicalRecord,
            IUpdateAppointment updateAppointment)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _getMedicalRecordByAppointmentId = getMedicalRecordByAppointmentId;
            _createMedicalRecord = createMedicalRecord;
            _updateAppointment = updateAppointment;
        }

        /// <summary>
        /// Process create medical record request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <param name="request">Create medical record request</param>
        /// <returns>Create medical record response</returns>
        public async Task<ApiResponse<CreateMedicalRecordResponse>> Process(Guid userId, Guid appointmentId, CreateMedicalRecordRequest request)
        {
            // 1. Initialize validation flags
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            bool isStatusValid = true;
            bool isMedicalRecordExists = false;
            // 2. Retrieve doctor entity
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment entity
            var retrievedAppointment = await RetrieveAppointment(appointmentId);
            // 4. Retrieve medical record entity
            var retrievedRecord = await RetrieveMedicalRecord(appointmentId);
            // 5. Validate retrieved data
            ValidateDoctor(retrievedDoctor, ref isDoctorValid);
            ValidateAppointment(retrievedAppointment, ref isAppointmentValid);
            ValidateOwnership(retrievedDoctor, retrievedAppointment, ref isOwnershipValid);
            ValidateAppointmentStatus(retrievedAppointment, ref isStatusValid);
            ValidateMedicalRecord(retrievedRecord, ref isMedicalRecordExists);
            // 6. Create medical record
            var createdMedicalRecord = await CreateMedicalRecord(
                retrievedDoctor,
                retrievedAppointment,
                request,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusValid,
                isMedicalRecordExists);
            // 7. Update appointment status
            await UpdateAppointment(
                retrievedAppointment,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusValid,
                isMedicalRecordExists);
            // 8. Map response
            var mappedResponse = MapToResponse(
                retrievedDoctor,
                retrievedAppointment,
                createdMedicalRecord);
            // 9. Create API response
            return CreateResponse(
                mappedResponse,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusValid,
                isMedicalRecordExists);
        }

        /// <summary>
        /// Retrieve doctor by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve appointment entity
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Appointment entity</returns>
        private async Task<Appointment> RetrieveAppointment(Guid appointmentId)
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
        private void ValidateDoctor(Doctor doctor, ref bool isDoctorValid)
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
        private void ValidateAppointment(Appointment appointment, ref bool isAppointmentValid)
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
        private void ValidateOwnership(Doctor doctor, Appointment appointment, ref bool isOwnershipValid)
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
        /// Validate appointment status before creating medical record
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isStatusValid">Validation flag</param>
        private void ValidateAppointmentStatus(Appointment appointment, ref bool isStatusValid)
        {
            if (appointment != null)
            {
                if (appointment.Status != AppointmentStatus.InProgress)
                {
                    isStatusValid = false;
                }
            }
        }

        /// <summary>
        /// Validate medical record existence for the appointment
        /// </summary>
        /// <param name="record">Medical record entity</param>
        /// <param name="isMedicalRecordExists">Validation flag</param>
        private void ValidateMedicalRecord(MedicalRecord record, ref bool isMedicalRecordExists)
        {
            if (record != null)
            {
                isMedicalRecordExists = true;
            }
        }

        /// <summary>
        /// Create medical record entity
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="request">Create medical record request</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        /// <param name="isStatusValid">Appointment status validation flag</param>
        /// <param name="isMedicalRecordExists">Medical record existence flag</param>
        /// <returns>Created medical record entity</returns>
        private async Task<MedicalRecord> CreateMedicalRecord(
            Doctor doctor,
            Appointment appointment,
            CreateMedicalRecordRequest request,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusValid,
            bool isMedicalRecordExists)
        {
            if (isDoctorValid &&
                isAppointmentValid &&
                isOwnershipValid &&
                isStatusValid &&
                !isMedicalRecordExists &&
                appointment != null &&
                doctor != null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Id = Guid.NewGuid(),
                    AppointmentId = appointment.Id,
                    DoctorId = doctor.Id,
                    PatientId = appointment.PatientId,
                    Symptoms = request.Symptoms,
                    Diagnosis = request.Diagnosis,
                    Notes = request.Notes,
                    CreateBy = doctor.Id.ToString(),
                    LastModifiedBy = doctor.Id.ToString()
                };
                await _createMedicalRecord.Execute(medicalRecord);
                return medicalRecord;
            }
            return null;
        }

        /// <summary>
        /// Update appointment status after medical record creation
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        /// <param name="isStatusValid">Appointment status validation flag</param>
        /// <param name="isMedicalRecordExists">Medical record existence flag</param>
        private async Task UpdateAppointment(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusValid,
            bool isMedicalRecordExists)
        {
            if (isDoctorValid &&
                isAppointmentValid &&
                isOwnershipValid &&
                isStatusValid &&
                !isMedicalRecordExists &&
                appointment != null)
            {
                appointment.Status = AppointmentStatus.Completed;
                await _updateAppointment.Execute(appointment);
            }
        }

        /// <summary>
        /// Map entities to response model
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="medicalRecord">Medical record entity</param>
        /// <returns>Create medical record response</returns>
        private CreateMedicalRecordResponse MapToResponse(
            Doctor doctor,
            Appointment appointment,
            MedicalRecord medicalRecord)
        {
            if (doctor == null || appointment == null || medicalRecord == null)
            {
                return null;
            }
            return new CreateMedicalRecordResponse
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

                Symptoms = medicalRecord.Symptoms,
                Diagnosis = medicalRecord.Diagnosis,
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
        /// <param name="isMedicalRecordExists">Medical record existence flag</param>
        /// <returns>API response</returns>
        private ApiResponse<CreateMedicalRecordResponse> CreateResponse(
            CreateMedicalRecordResponse response,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusValid,
            bool isMedicalRecordExists)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<CreateMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            if (!isAppointmentValid)
            {
                return ApiResponse<CreateMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }
            if (!isOwnershipValid)
            {
                return ApiResponse<CreateMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            if (!isStatusValid)
            {
                return ApiResponse<CreateMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4013.ToString());
            }
            if (isMedicalRecordExists)
            {
                return ApiResponse<CreateMedicalRecordResponse>
                    .Fail(MessageCode.APP_MESSAGE_4016.ToString());
            }
            return ApiResponse<CreateMedicalRecordResponse>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}