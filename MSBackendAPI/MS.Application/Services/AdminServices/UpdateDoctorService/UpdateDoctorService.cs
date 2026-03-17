using MS.Application.Common.Response;
using MS.Application.Services.AdminServices.GetDoctorById;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateDoctor;

namespace MS.Application.Services.AdminServices.UpdateDoctorService
{
    /// <summary>
    /// Provides an implementation of the IUpdateDoctorService interface
    /// for handling doctor update operations.
    /// </summary>
    public class UpdateDoctorService : IUpdateDoctorService
    {
        private readonly IUpdateDoctor _updateDoctor;
        private readonly IGetDoctorById _getDoctorById;

        /// <summary>
        /// Initializes a new instance of the UpdateDoctorService class.
        /// </summary>
        /// <param name="updateDoctor">
        /// Repository responsible for updating doctor data.
        /// </param>
        /// <param name="getDoctorById">
        /// Repository used to retrieve doctor information by ID.
        /// </param>
        public UpdateDoctorService(
            IUpdateDoctor updateDoctor,
            IGetDoctorById getDoctorById)
        {
            _updateDoctor = updateDoctor;
            _getDoctorById = getDoctorById;
        }

        /// <summary>
        /// Processes the update doctor request.
        /// </summary>
        /// <param name="request">
        /// The request object containing updated doctor information.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the updated doctor ID.
        /// </returns>
        public async Task<ApiResponse<Guid>> Process(UpdateDoctorRequest request)
        {
            // 1. Initialize validation flags
            bool isNotFound = false;
            // 2. Retrieve doctor by id
            var doctor = await RetrieveData(request.Id);
            // 3. Validate retrieved doctor data
            ValidateData(doctor, ref isNotFound);
            // 4. Create response
            return await CreateResponse(doctor, request, isNotFound);
        }

        /// <summary>
        /// Retrieves a doctor by their unique identifier.
        /// </summary>
        /// <param name="id">
        /// The doctor ID.
        /// </param>
        /// <returns>
        /// A <see cref="Doctor"/> entity if found; otherwise null.
        /// </returns>
        private async Task<Doctor> RetrieveData(Guid id)
        {
            return await _getDoctorById.Execute(id);
        }

        /// <summary>
        /// Validates whether the doctor exists.
        /// </summary>
        /// <param name="doctor">
        /// The retrieved doctor entity.
        /// </param>
        /// <param name="isNotFound">
        /// A flag indicating whether the doctor exists in the system.
        /// </param>
        private void ValidateData(Doctor doctor, ref bool isNotFound)
        {
            if (doctor == null)
            {
                isNotFound = true;
            }
        }

        /// <summary>
        /// Creates the final API response after processing the update operation.
        /// </summary>
        /// <param name="doctor">
        /// The doctor entity retrieved from the database.
        /// </param>
        /// <param name="request">
        /// The request containing updated doctor information.
        /// </param>
        /// <param name="isNotFound">
        /// Indicates whether the doctor exists in the system.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> representing the result of the update operation.
        /// </returns>
        private async Task<ApiResponse<Guid>> CreateResponse(
            Doctor doctor,
            UpdateDoctorRequest request,
            bool isNotFound)
        {
            if (isNotFound)
            {
                return ApiResponse<Guid>.Fail("APP_MESSAGE_4040");
            }
            // Map DTO data to entity
            MapUpdateData(doctor, request);
            // Persist changes
            await _updateDoctor.Execute(doctor);
            return ApiResponse<Guid>.Success(
                "APP_MESSAGE_2000",
                doctor.Id
            );
        }

        /// <summary>
        /// Maps the DTO fields from the update request to the doctor entity.
        /// </summary>
        /// <param name="doctor">
        /// The doctor entity to be updated.
        /// </param>
        /// <param name="request">
        /// The request containing updated doctor information.
        /// </param>
        private void MapUpdateData(Doctor doctor, UpdateDoctorRequest request)
        {
            doctor.FullName = request.FullName;
            doctor.Email = request.Email;
            doctor.PhoneNumber = request.PhoneNumber;
            doctor.AvatarUrl = request.AvatarUrl;
            doctor.PhotoUrl = request.PhotoUrl;
            doctor.BioVi = request.BioVi;
            doctor.BioEn = request.BioEn;
            doctor.AcademicTitleVi = request.AcademicTitleVi;
            doctor.AcademicTitleEn = request.AcademicTitleEn;
            doctor.YearsOfExperience = request.YearsOfExperience;
            doctor.SpecialtyId = request.SpecialtyId;
            doctor.LastModifiedBy = "system";
            doctor.LastModifiedDate = DateTimeOffset.UtcNow;
        }
    }
}