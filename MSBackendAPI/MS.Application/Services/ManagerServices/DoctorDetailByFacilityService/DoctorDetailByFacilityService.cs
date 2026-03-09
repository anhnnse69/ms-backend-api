using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorDetailByFacility;

namespace MS.Application.Services.DoctorDetailByFacilityService
{
    /// <summary>
    /// Service responsible for retrieving doctor detail information
    /// </summary>
    public class DoctorDetailByFacilityService : IDoctorDetailByFacilityService
    {
        private readonly IGetDoctorDetailByFacility _repository;
        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="repository">
        /// Repository used to retrieve doctor detail by identifier
        /// </param>
        public DoctorDetailByFacilityService(IGetDoctorDetailByFacility repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Process request to retrieve doctor detail
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Doctor detail response</returns>
        public async Task<ApiResponse<DoctorDetailResponse>> Process(Guid doctorId)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Retrieve doctor entity
            var doctor = await RetrieveDoctor(doctorId);
            // 3. Validate retrieved data
            ValidateRetrievedData(doctor, ref isDataValid);
            // 4. Create API response
            return CreateResponse(doctor, isDataValid);
        }
        /// <summary>
        /// Retrieve doctor entity by identifier
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor?> RetrieveDoctor(Guid doctorId)
        {
            return await _repository.Execute(doctorId);
        }
        /// <summary>
        /// Validate retrieved doctor data
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateRetrievedData(Doctor? doctor, ref bool isDataValid)
        {
            if (doctor == null)
            {
                isDataValid = false;
            }
        }
        /// <summary>
        /// Create API response from doctor entity
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing doctor detail</returns>
        private ApiResponse<DoctorDetailResponse> CreateResponse(
            Doctor? doctor,
            bool isDataValid)
        {
            if (!isDataValid || doctor == null)
            {
                return ApiResponse<DoctorDetailResponse>.Fail(
                    MessageCode.APP_MESSAGE_4004.ToString()
                );
            }
            var response = new DoctorDetailResponse
            {
                Id = doctor.Id,
                DisplayName = doctor.DisplayName,
                FullName = doctor.FullName,
                BioVi = doctor.BioVi,
                AcademicTitleVi = doctor.AcademicTitleVi,
                AvatarUrl = doctor.AvatarUrl,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                YearsOfExperience = doctor.YearsOfExperience,
                SpecialtyName = doctor.Specialty?.NameVi,
                Languages = doctor.Languages
                    .Select(l => l.Language.ToString())
                    .ToList()
            };
            return ApiResponse<DoctorDetailResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response
            );
        }
    }
}