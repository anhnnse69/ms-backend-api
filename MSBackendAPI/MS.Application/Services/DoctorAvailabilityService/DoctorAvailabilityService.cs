using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.DoctorAvailabilityService
{
    /// <summary>
    /// Doctor availability service implementation
    /// </summary>
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IGetDoctorAvailabilities _getDoctorAvailabilities;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        public DoctorAvailabilityService(
            IGetDoctorAvailabilities getDoctorAvailabilities,
            IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAvailabilities = getDoctorAvailabilities;
            _getDoctorByUserId = getDoctorByUserId;
        }

        public async Task<ApiResponse<IEnumerable<DoctorAvailabilityResponse>>> GetMyAvailabilities(Guid userId)
        {
            bool isValid = true;

            // 1️. Lấy Doctor từ UserId
            var doctor = await _getDoctorByUserId.Execute(userId);

            if (doctor == null)
            {
                return ApiResponse<IEnumerable<DoctorAvailabilityResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            // 2️. Retrieve availability bằng DoctorId
            var retrievedData = await RetrieveData(doctor.Id);

            // 3️. Validate
            ValidateData(retrievedData, ref isValid);

            // 4️. Create response
            return await CreateResponse(retrievedData, isValid);
        }

        private async Task<IEnumerable<DoctorAvailability>> RetrieveData(Guid doctorId)
        {
            return await _getDoctorAvailabilities.Execute(doctorId);
        }

        private void ValidateData(IEnumerable<DoctorAvailability> data, ref bool isValid)
        {
            if (data == null || !data.Any())
            {
                isValid = false;
            }
        }

        private async Task<ApiResponse<IEnumerable<DoctorAvailabilityResponse>>> CreateResponse(
            IEnumerable<DoctorAvailability> data,
            bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<DoctorAvailabilityResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }

            var result = data.Select(x => new DoctorAvailabilityResponse
            {
                Id = x.Id,
                FacilityId = x.FacilityId,
                FacilityName = x.Facility?.NameVi,
                DayOfWeek = x.DayOfWeek,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                SlotDurationMinutes = x.SlotDurationMinutes,
                IsActive = x.IsActive
            });

            return ApiResponse<IEnumerable<DoctorAvailabilityResponse>>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}