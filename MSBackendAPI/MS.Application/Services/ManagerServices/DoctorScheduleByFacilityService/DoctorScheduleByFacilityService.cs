using MS.Application.Common.Response;
using MS.Application.Services.ManagerServices.DoctorScheduleByFacilityService;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorScheduleByFacility;

namespace MS.Application.Services.DoctorScheduleService
{
    /// <summary>
    /// Service responsible for retrieving doctor schedules by facility
    /// </summary>
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IGetDoctorScheduleByFacility _repository;
        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="repository">
        /// Repository used to retrieve doctor schedules by facility identifier
        /// </param>
        public DoctorScheduleService(IGetDoctorScheduleByFacility repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Process request to retrieve doctor schedules
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of doctor schedules</returns>
        public async Task<ApiResponse<List<DoctorScheduleResponse>>> Process(Guid facilityId, int page, int size)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Retrieve doctors with schedules
            var (doctors, total) = await _repository.Execute(facilityId, page, size);
            // 3. Validate retrieved data
            ValidateRetrievedData(doctors, ref isDataValid);
            // 4. Create API response
            return CreateResponse(doctors, page, size, total, isDataValid);
        }
        /// <summary>
        /// Validate retrieved doctors data
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateRetrievedData(List<Doctor> doctors, ref bool isDataValid)
        {
            if (doctors == null || !doctors.Any())
                isDataValid = false;
        }
        /// <summary>
        /// Create API response from doctor entities
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <param name="total">Total number of records</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing doctor schedules</returns>
        private ApiResponse<List<DoctorScheduleResponse>> CreateResponse(
            List<Doctor> doctors,
            int page,
            int size,
            int total,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<List<DoctorScheduleResponse>>.Fail(
                    MessageCode.APP_MESSAGE_4004.ToString());
            }
            // Map doctor entities to response model
            var mappedDoctors = MapDoctorSchedules(doctors);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<List<DoctorScheduleResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                mappedDoctors,
                meta);
        }
        /// <summary>
        /// Map doctor entities to doctor schedule response models
        /// </summary>
        /// <param name="doctors">List of doctor entities</param>
        /// <returns>List of doctor schedule response models</returns>
        private List<DoctorScheduleResponse> MapDoctorSchedules(List<Doctor> doctors)
        {
            return doctors.Select(d => new DoctorScheduleResponse
            {
                DoctorId = d.Id,
                DoctorName = d.DisplayName,
                Schedules = d.Availabilities.Select(a => new ScheduleSlotResponse
                {
                    DayOfWeek = a.DayOfWeek,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    SlotDurationMinutes = a.SlotDurationMinutes
                }).ToList()
            }).ToList();
        }
    }
}