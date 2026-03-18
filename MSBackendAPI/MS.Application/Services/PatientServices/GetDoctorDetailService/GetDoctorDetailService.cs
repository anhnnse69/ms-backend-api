using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetDoctorWithDetail;

namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Service responsible for handling the get doctor detail business logic.
    /// </summary>
    public class GetDoctorDetailService : IGetDoctorDetailService
    {
        private readonly IGetDoctorWithDetail _getDoctorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDoctorDetailService"/> class.
        /// </summary>
        /// <param name="getDoctorRepo">Repository responsible for doctor detail data access.</param>
        public GetDoctorDetailService(IGetDoctorWithDetail getDoctorRepo)
        {
            _getDoctorRepo = getDoctorRepo;
        }

        /// <summary>
        /// Processes the request to retrieve detailed profile information of a doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor to retrieve.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the doctor's full profile,
        /// specialties, facilities, and weekly availability schedule.
        /// </returns>
        public async Task<ApiResponse<GetDoctorDetailResponse>> Process(Guid doctorId)
        {
            // 1. Initialize validation flags
            bool isDoctorValid = true;
            // 2. Retrieve data from repositories
            var doctor = await RetrieveDoctor(doctorId);
            // 3. Validate retrieved data
            ValidateDoctor(doctor, ref isDoctorValid);
            // 4. Map to response
            var response = MapToResponse(doctor, isDoctorValid);
            // 5. Return API response
            return CreateResponse(response, isDoctorValid);
        }

        /// <summary>
        /// Retrieves the doctor entity with all related data by its identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>Doctor entity with related data if found; otherwise null.</returns>
        private async Task<Doctor?> RetrieveDoctor(Guid doctorId)
        {
            return await _getDoctorRepo.Execute(doctorId);
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
        /// Maps the doctor entity to <see cref="GetDoctorDetailResponse"/>.
        /// </summary>
        /// <param name="doctor">The doctor entity to map.</param>
        /// <param name="isDoctorValid">Flag indicating whether the doctor is valid.</param>
        /// <returns>Mapped <see cref="GetDoctorDetailResponse"/>; default instance if doctor is null.</returns>
        private GetDoctorDetailResponse MapToResponse(Doctor? doctor, bool isDoctorValid)
        {
            if (!isDoctorValid || doctor == null)
            {
                return new GetDoctorDetailResponse();
            }
            return new GetDoctorDetailResponse
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Avatar = doctor.AvatarUrl,
                Biography = doctor.BioVi,
                ExperienceYears = doctor.YearsOfExperience,
                AverageRating = doctor.AverageRating,
                Specialties = new List<DoctorSpecialtyResponse>
                {
                    new DoctorSpecialtyResponse
                    {
                        Id = doctor.Specialty!.Id,
                        Name = doctor.Specialty.NameVi
                    }
                },
                Facilities = doctor.Facilities
                    .Select(df => new DoctorFacilityResponse
                    {
                        Id = df.FacilityId,
                        Name = df.Facility.NameVi
                    })
                    .ToList(),
                Availabilities = doctor.Availabilities
                    .Select(a => new DoctorAvailabilityResponse
                    {
                        DayOfWeek = (int)a.DayOfWeek,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        SlotDurationMinutes = a.SlotDurationMinutes
                    })
                    .ToList()
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isDoctorValid">Flag indicating whether the doctor is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<GetDoctorDetailResponse> CreateResponse(
            GetDoctorDetailResponse response,
            bool isDoctorValid)
        {
            if (!isDoctorValid)
                return ApiResponse<GetDoctorDetailResponse>.Fail(MessageCode.APP_MESSAGE_4011.ToString());
            return ApiResponse<GetDoctorDetailResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
