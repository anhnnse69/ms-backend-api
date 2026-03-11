using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.Doctors.GetDoctorAvailabilityService
{
    /// <summary>
    /// Get doctor availability service implementation
    /// </summary>
    public class GetDoctorAvailabilityService : IGetDoctorAvailabilityService
    {
        private readonly IGetDoctorAvailabilities _getDoctorAvailabilities;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        /// <summary>
        /// Get doctor availability service constructor
        /// </summary>
        /// <param name="getDoctorAvailabilities"></param>
        /// <param name="getDoctorByUserId"></param>
        public GetDoctorAvailabilityService(
            IGetDoctorAvailabilities getDoctorAvailabilities,
            IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAvailabilities = getDoctorAvailabilities;
            _getDoctorByUserId = getDoctorByUserId;
        }

        /// <summary>
        /// Process get doctor availability request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>> Process(Guid userId, int page, int size)
        {
            // 1. Initialize validation flags
            bool isRetrievedDataValid = true;
            // 2. Retrieve doctor entity
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve availability query
            var retrievedQuery = RetrieveQuery(retrievedDoctor);
            // 4. Retrieve paginated data
            var retrievedData = await RetrieveData(retrievedQuery, page, size);
            // 5. Retrieve total records
            var total = await RetrieveTotal(retrievedQuery);
            // 6. Validate retrieved data
            ValidateData(retrievedDoctor, ref isRetrievedDataValid);
            // 7. Create response
            return CreateResponse(retrievedData, total, page, size, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve doctor entity by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve doctor availability query
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        private IQueryable<DoctorAvailability> RetrieveQuery(Doctor doctor)
        {
            if (doctor == null)
            {
                return null;
            }

            return _getDoctorAvailabilities.Execute(doctor.Id);
        }

        /// <summary>
        /// Retrieve paginated availability data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private async Task<List<DoctorAvailability>> RetrieveData(IQueryable<DoctorAvailability> query, int page, int size)
        {
            if (query == null)
            {
                return null;
            }
            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve total number of availability records
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<int> RetrieveTotal(IQueryable<DoctorAvailability> query)
        {
            if (query == null)
            {
                return 0;
            }
            return await query.CountAsync();
        }

        /// <summary>
        /// Validate retrieved doctor data
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="isRetrievedDataValid"></param>
        private void ValidateData(Doctor doctor, ref bool isRetrievedDataValid)
        {
            if (doctor == null)
            {
                isRetrievedDataValid = false;
            }
        }

        /// <summary>
        /// Map availability entity to response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
                // Working day
                DayOfWeek = x.DayOfWeek,
                // Start time
                StartTime = x.StartTime,
                // End time
                EndTime = x.EndTime,
                // Slot duration
                SlotDurationMinutes = x.SlotDurationMinutes,
                // Availability status
                IsDeleted = x.IsDeleted
            });
        }

        /// <summary>
        /// Create response of the get doctor availability request
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="isRetrievedDataValid"></param>
        /// <returns></returns>
        private ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>> CreateResponse(
            IEnumerable<DoctorAvailability> data,
            int total,
            int page,
            int size,
            bool isRetrievedDataValid)
        {
            if (!isRetrievedDataValid)
            {
                return ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            var result = MapToResponse(data);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<IEnumerable<GetDoctorAvailabilityResponse>>
                .Success(
                    MessageCode.APP_MESSAGE_2000.ToString(),
                    result,
                    meta
                );
        }
    }
}