using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.Doctors.GetDoctorAvailabilityService
{
    /// <summary>
    /// Service responsible for retrieving doctor availability schedule
    /// </summary>
    public class GetDoctorAvailabilityService : IGetDoctorAvailabilityService
    {
        private readonly IGetDoctorAvailabilities _getDoctorAvailabilities;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        /// <summary>
        /// Get doctor availability service constructor
        /// </summary>
        /// <param name="getDoctorAvailabilities">Repository to retrieve doctor availability</param>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        public GetDoctorAvailabilityService(IGetDoctorAvailabilities getDoctorAvailabilities,IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAvailabilities = getDoctorAvailabilities;
            _getDoctorByUserId = getDoctorByUserId;
        }

        /// <summary>
        /// Process get doctor availability request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <returns>Doctor availability schedule</returns>
        public async Task<ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>> Process(Guid userId)
        {
            // 1. Initialize validation flags
            bool isRetrievedDataValid = true;
            // 2. Retrieve doctor entity using user identifier
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve availability data
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
        /// Retrieve doctor availability list
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <returns>Collection of doctor availability entities</returns>
        private async Task<IEnumerable<DoctorAvailability>> RetrieveData(Doctor doctor)
        {
            if (doctor == null)
            {
                return null;
            }
            return await _getDoctorAvailabilities.Execute(doctor.Id);
        }

        /// <summary>
        /// Validate retrieved availability data
        /// </summary>
        /// <param name="data">Availability entities</param>
        /// <param name="isRetrievedDataValid">Validation flag</param>
        private void ValidateData(IEnumerable<DoctorAvailability> data, ref bool isRetrievedDataValid)
        {
            if (data == null || !data.Any())
            {
                isRetrievedDataValid = false;
            }
        }

        /// <summary>
        /// Map availability entities to response models
        /// </summary>
        /// <param name="data">Availability entities</param>
        /// <returns>Mapped availability response models</returns>
        private IEnumerable<GetDoctorAvailabilityResponse> MapToResponse(IEnumerable<DoctorAvailability> data)
        {
            return data.Select(x => new GetDoctorAvailabilityResponse
            {
                // Availability identifier
                Id = x.Id,
                // Facility identifier
                FacilityId = x.FacilityId,
                // Facility name
                FacilityName = x.Facility?.NameVi,
                // Working day of doctor
                DayOfWeek = x.DayOfWeek,
                // Start time
                StartTime = x.StartTime,
                // End time
                EndTime = x.EndTime,
                // Slot duration in minutes
                SlotDurationMinutes = x.SlotDurationMinutes,
                // Availability status
                IsActive = x.IsActive
            });
        }

        /// <summary>
        /// Create API response for doctor availability
        /// </summary>
        /// <param name="data">Availability entities</param>
        /// <param name="isRetrievedDataValid">Validation flag</param>
        /// <returns>API response containing availability list</returns>
        private ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>> CreateResponse(IEnumerable<DoctorAvailability> data,bool isRetrievedDataValid)
        {
            if (!isRetrievedDataValid)
            {
                return ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            // Map entity data to response model
            var result = MapToResponse(data);
            return ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}