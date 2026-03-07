using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.Doctors.GetDoctorAppointmentService
{
    /// <summary>
    /// Service responsible for retrieving doctor appointments
    /// </summary>
    public class GetDoctorAppointmentService : IGetDoctorAppointmentService
    {
        private readonly IGetDoctorAppointments _getDoctorAppointments;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        /// <summary>
        /// Get doctor appointment service constructor
        /// </summary>
        /// <param name="getDoctorAppointments">Repository to retrieve doctor appointments</param>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor information by user id</param>
        public GetDoctorAppointmentService(IGetDoctorAppointments getDoctorAppointments,IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAppointments = getDoctorAppointments;
            _getDoctorByUserId = getDoctorByUserId;
        }

        /// <summary>
        /// Process get doctor appointment request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <returns>List of doctor appointments</returns>
        public async Task<ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>> Process(Guid userId)
        {
            // 1. Initialize validation flags
            bool isRetrievedDataValid = true;
            // 2. Retrieve doctor entity using user identifier
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment data
            var retrievedData = await RetrieveData(retrievedDoctor);
            // 4. Validate retrieved data
            ValidateData(retrievedData, ref isRetrievedDataValid);
            // 5. Create response
            return CreateResponse(retrievedData, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve doctor entity by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve doctor appointment list
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <returns>Collection of appointment entities</returns>
        private async Task<IEnumerable<Appointment>> RetrieveData(Doctor doctor)
        {
            if (doctor == null)
            {
                return null;
            }
            return await _getDoctorAppointments.Execute(doctor.Id);
        }

        /// <summary>
        /// Validate retrieved appointment data
        /// </summary>
        /// <param name="data">Appointment data</param>
        /// <param name="isRetrievedDataValid">Validation flag</param>
        private void ValidateData(IEnumerable<Appointment> data, ref bool isRetrievedDataValid)
        {
            if (data == null || !data.Any())
            {
                isRetrievedDataValid = false;
            }
        }

        /// <summary>
        /// Map appointment entities to response models
        /// </summary>
        /// <param name="data">Appointment entities</param>
        /// <returns>Mapped appointment response models</returns>
        private IEnumerable<GetDoctorAppointmentResponse> MapToResponse(IEnumerable<Appointment> data)
        {
            return data.Select(x => new GetDoctorAppointmentResponse
            {
                // Appointment identifier
                AppointmentId = x.Id,
                // Patient full name
                PatientName = x.Patient?.FullName,
                // Facility name
                FacilityName = x.Facility?.NameVi,
                // Appointment time
                AppointmentTime = x.AppointmentTime,
                // Appointment status
                Status = x.Status
            });
        }

        /// <summary>
        /// Create API response for doctor appointments
        /// </summary>
        /// <param name="data">Appointment entities</param>
        /// <param name="isRetrievedDataValid">Validation flag</param>
        /// <returns>API response containing appointment list</returns>
        private ApiResponse<IEnumerable<GetDoctorAppointmentResponse>> CreateResponse(IEnumerable<Appointment> data,bool isRetrievedDataValid)
        {
            if (!isRetrievedDataValid)
            {
                return ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            // Map entity data to response model
            var result = MapToResponse(data);
            return ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}