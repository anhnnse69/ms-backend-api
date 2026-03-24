using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.PatientRepositories.CreateAppointment;
using MS.Infrastructure.Repositories.PatientRepositories.GetDoctorForBooking;
using MS.Infrastructure.Repositories.PatientRepositories.GetFacilityForBooking;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientByUserId;
using MS.Infrastructure.Repositories.PatientRepositories.GetSpecialtyForBooking;

namespace MS.Application.Services.PatientServices.BookAppointmentService
{
    /// <summary>
    /// Service responsible for handling the book appointment business logic.
    /// </summary>
    public class BookAppointmentService : IBookAppointmentService
    {
        private readonly IGetPatientByUserId _getPatientRepo;
        private readonly IGetFacilityForBooking _getFacilityRepo;
        private readonly IGetSpecialtyForBooking _getSpecialtyRepo;
        private readonly IGetDoctorForBooking _getDoctorRepo;
        private readonly ICreateAppointment _createAppointmentRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookAppointmentService"/> class.
        /// </summary>
        /// <param name="getPatientRepo">Repository responsible for patient data access.</param>
        /// <param name="getFacilityRepo">Repository responsible for facility data access.</param>
        /// <param name="getSpecialtyRepo">Repository responsible for specialty data access.</param>
        /// <param name="getDoctorRepo">Repository responsible for doctor data access.</param>
        /// <param name="createAppointmentRepo">Repository responsible for appointment creation.</param>
        public BookAppointmentService(
            IGetPatientByUserId getPatientRepo,
            IGetFacilityForBooking getFacilityRepo,
            IGetSpecialtyForBooking getSpecialtyRepo,
            IGetDoctorForBooking getDoctorRepo,
            ICreateAppointment createAppointmentRepo)
        {
            _getPatientRepo = getPatientRepo;
            _getFacilityRepo = getFacilityRepo;
            _getSpecialtyRepo = getSpecialtyRepo;
            _getDoctorRepo = getDoctorRepo;
            _createAppointmentRepo = createAppointmentRepo;
        }

        /// <summary>
        /// Processes the request to book a new medical appointment for a patient.
        /// </summary>
        /// <param name="request">The request containing appointment booking details.</param>
        /// <param name="patientUserId">The user identifier of the authenticated patient.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created appointment id and status.
        /// </returns>
        public async Task<ApiResponse<BookAppointmentResponse>> Process(BookAppointmentRequest request, Guid patientUserId)
        {
            // 1. Initialize validation flags
            bool isPatientValid = true;
            bool isFacilityValid = true;
            bool isSpecialtyValid = true;
            bool isDoctorValid = true;
            // 2. Retrieve data from repositories
            var patient = await RetrievePatient(patientUserId);
            var facility = await RetrieveFacility(request.FacilityId);
            var specialty = await RetrieveSpecialty(request.SpecialtyId);
            var doctor = await RetrieveDoctor(request.DoctorId);
            // 3. Validate retrieved data
            ValidatePatient(patient, ref isPatientValid);
            ValidateFacility(facility, ref isFacilityValid);
            ValidateSpecialty(specialty, ref isSpecialtyValid);
            ValidateDoctor(doctor, ref isDoctorValid);
            // 4. Execute side effects
            var appointment = await ExecuteCreate(request, patient, isPatientValid, isFacilityValid, isSpecialtyValid, isDoctorValid);
            // 5. Map to response
            var response = MapToResponse(appointment);
            // 6. Return API response
            return CreateResponse(response, isPatientValid, isFacilityValid, isSpecialtyValid, isDoctorValid);
        }

        /// <summary>
        /// Retrieves the patient entity by the authenticated user identifier.
        /// </summary>
        /// <param name="userId">The user identifier of the authenticated patient.</param>
        /// <returns>Patient entity if found; otherwise null.</returns>
        private async Task<Patient?> RetrievePatient(Guid userId)
        {
            return await _getPatientRepo.Execute(userId);
        }

        /// <summary>
        /// Retrieves the facility entity by its identifier.
        /// </summary>
        /// <param name="facilityId">The unique identifier of the facility.</param>
        /// <returns>Facility entity if found; otherwise null.</returns>
        private async Task<Facility?> RetrieveFacility(Guid facilityId)
        {
            return await _getFacilityRepo.Execute(facilityId);
        }

        /// <summary>
        /// Retrieves the specialty entity by its identifier.
        /// </summary>
        /// <param name="specialtyId">The unique identifier of the specialty.</param>
        /// <returns>Specialty entity if found; otherwise null.</returns>
        private async Task<Specialty?> RetrieveSpecialty(Guid specialtyId)
        {
            return await _getSpecialtyRepo.Execute(specialtyId);
        }

        /// <summary>
        /// Retrieves the doctor entity by its identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>Doctor entity if found; otherwise null.</returns>
        private async Task<Doctor?> RetrieveDoctor(Guid doctorId)
        {
            return await _getDoctorRepo.Execute(doctorId);
        }

        /// <summary>
        /// Validates whether the patient entity exists.
        /// </summary>
        /// <param name="patient">The patient entity to validate.</param>
        /// <param name="isPatientValid">Flag; set to false if patient is null.</param>
        private void ValidatePatient(Patient? patient, ref bool isPatientValid)
        {
            if (patient == null)
            {
                isPatientValid = false;
            }
        }

        /// <summary>
        /// Validates whether the facility entity exists.
        /// </summary>
        /// <param name="facility">The facility entity to validate.</param>
        /// <param name="isFacilityValid">Flag; set to false if facility is null.</param>
        private void ValidateFacility(Facility? facility, ref bool isFacilityValid)
        {
            if (facility == null)
            {
                isFacilityValid = false;
            }
        }

        /// <summary>
        /// Validates whether the specialty entity exists.
        /// </summary>
        /// <param name="specialty">The specialty entity to validate.</param>
        /// <param name="isSpecialtyValid">Flag; set to false if specialty is null.</param>
        private void ValidateSpecialty(Specialty? specialty, ref bool isSpecialtyValid)
        {
            if (specialty == null)
            {
                isSpecialtyValid = false;
            }
        }

        /// <summary>
        /// Validates whether the doctor entity exists.
        /// </summary>
        /// <param name="doctor">The doctor entity to validate.</param>
        /// <param name="isDoctorValid">Flag; set to false if doctor is null.</param>
        private void ValidateDoctor(Doctor? doctor, ref bool isDoctorValid)
        {
            if (doctor == null)
            {
                isDoctorValid = false;
            }
        }

        /// <summary>
        /// Executes the appointment creation when all validation flags are valid.
        /// </summary>
        /// <param name="request">The booking request data.</param>
        /// <param name="patient">The resolved patient entity.</param>
        /// <param name="isPatientValid">Flag indicating patient is valid.</param>
        /// <param name="isFacilityValid">Flag indicating facility is valid.</param>
        /// <param name="isSpecialtyValid">Flag indicating specialty is valid.</param>
        /// <param name="isDoctorValid">Flag indicating doctor is valid.</param>
        /// <returns>The created appointment entity; otherwise null.</returns>
        private async Task<Appointment?> ExecuteCreate(
            BookAppointmentRequest request,
            Patient? patient,
            bool isPatientValid,
            bool isFacilityValid,
            bool isSpecialtyValid,
            bool isDoctorValid)
        {
            if (!isPatientValid || !isFacilityValid || !isSpecialtyValid || !isDoctorValid)
            {
                return null;
            }
            // Retrieve doctor again to access the configured booking deposit amount
            var doctor = await RetrieveDoctor(request.DoctorId);
            var appointment = new Appointment
            {
                PatientId = patient!.Id,
                FacilityId = request.FacilityId,
                SpecialtyId = request.SpecialtyId,
                DoctorId = request.DoctorId,
                AppointmentTime = request.AppointmentTime,
                Notes = request.Notes,
                // Copy per-doctor deposit to the appointment so that
                // the required amount is frozen at booking time.
                DepositAmount = doctor?.BookingDepositAmount,
                IsDepositPaid = false,
                PaymentMethod = null,
                Status = AppointmentStatus.PendingConfirmation,
                CreateBy = patient.Id.ToString()
            };
            return await _createAppointmentRepo.Execute(appointment);
        }

        /// <summary>
        /// Maps the created appointment entity to <see cref="BookAppointmentResponse"/>.
        /// </summary>
        /// <param name="appointment">The created appointment entity to map.</param>
        /// <returns>Mapped <see cref="BookAppointmentResponse"/>; default instance with safe values if appointment is null.</returns>
        private BookAppointmentResponse MapToResponse(Appointment? appointment)
        {
            if (appointment == null)
            {
                // Return a response with explicitly initialized non-nullable properties
                return new BookAppointmentResponse
                {
                    Status = string.Empty
                };
            }
            return new BookAppointmentResponse
            {
                Id = appointment.Id,
                Status = appointment.Status.ToString()
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isPatientValid">Flag indicating patient is valid.</param>
        /// <param name="isFacilityValid">Flag indicating facility is valid.</param>
        /// <param name="isSpecialtyValid">Flag indicating specialty is valid.</param>
        /// <param name="isDoctorValid">Flag indicating doctor is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<BookAppointmentResponse> CreateResponse(
            BookAppointmentResponse response,
            bool isPatientValid,
            bool isFacilityValid,
            bool isSpecialtyValid,
            bool isDoctorValid)
        {
            if (!isPatientValid)
                return ApiResponse<BookAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4010.ToString());
            if (!isFacilityValid)
                return ApiResponse<BookAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4008.ToString());
            if (!isSpecialtyValid)
                return ApiResponse<BookAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4009.ToString());
            if (!isDoctorValid)
                return ApiResponse<BookAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4011.ToString());
            return ApiResponse<BookAppointmentResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}