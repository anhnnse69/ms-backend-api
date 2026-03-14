using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetPendingAppointmentPatient;

namespace MS.Application.Services.ManagerServices.PendingAppointmentPatientService
{
    /// <summary>
    /// Service responsible for retrieving pending patient appointments
    /// </summary>
    public class GetPendingAppointmentPatientService : IGetPendingAppointmentPatientService
    {
        private readonly IGetPendingAppointmentPatient _repository;
        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="repository">
        /// Repository used to retrieve pending patient appointments
        /// </param>
        public GetPendingAppointmentPatientService(IGetPendingAppointmentPatient repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Process request to retrieve pending patient appointments
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of pending appointment patients</returns>
        public async Task<ApiResponse<List<PendingAppointmentResponse>>> Process(int page, int size)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Retrieve appointments from repository
            var (appointments, total) = await _repository.Execute(page, size);
            // 3. Validate retrieved data
            ValidateRetrievedData(appointments, ref isDataValid);
            // 4. Create API response
            return CreateResponse(appointments, page, size, total, isDataValid);
        }

        /// <summary>
        /// Validate retrieved appointment data
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateRetrievedData(List<Appointment> appointments, ref bool isDataValid)
        {
            if (appointments == null || !appointments.Any())
                isDataValid = false;
        }

        /// <summary>
        /// Create API response from appointment entities
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <param name="total">Total number of records</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing pending appointment data</returns>
        private ApiResponse<List<PendingAppointmentResponse>> CreateResponse(
            List<Appointment> appointments,
            int page,
            int size,
            int total,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<List<PendingAppointmentResponse>>.Fail(
                    MessageCode.APP_MESSAGE_4004.ToString());
            }
            // Map appointment entities to response model
            var mappedAppointments = MapAppointments(appointments);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<List<PendingAppointmentResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                mappedAppointments,
                meta);
        }

        /// <summary>
        /// Map appointment entities to pending appointment response models
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <returns>List of pending appointment response models</returns>
        private List<PendingAppointmentResponse> MapAppointments(List<Appointment> appointments)
        {
            return appointments.Select(a => new PendingAppointmentResponse
            {
                AppointmentId = a.Id,
                AppointmentTime = a.AppointmentTime,
                PatientName = a.Patient.FullName,
                PhoneNumber = a.Patient.PhoneNumber,
                DoctorName = a.Doctor != null ? a.Doctor.FullName : null,
                FacilityName = a.Facility.NameVi,
                SpecialtyName = a.Specialty.NameVi,
                Status = a.Status
            }).ToList();
        }
    }
}