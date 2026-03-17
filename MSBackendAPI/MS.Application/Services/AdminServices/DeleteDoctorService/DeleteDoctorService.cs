using MS.Application.Common.Response;
using MS.Application.Services.AdminServices.GetDoctorById;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateDoctor;

namespace MS.Application.Services.AdminServices.DeleteDoctorService
{
    /// <summary>
    /// Service for deleting doctor (soft delete).
    /// </summary>
    public class DeleteDoctorService : IDeleteDoctorService
    {
        private readonly IGetDoctorById _getDoctorById;
        private readonly IUpdateDoctor _updateDoctor;

        /// <summary>
        /// Initializes a new instance of the DeleteDoctorService class.
        /// </summary>
        /// <param name="getDoctorById">
        /// Repository used to retrieve doctor by id.
        /// </param>
        /// <param name="updateDoctor">
        /// Repository used to update doctor data.
        /// </param>
        public DeleteDoctorService(IGetDoctorById getDoctorById, IUpdateDoctor updateDoctor)
        {
            _getDoctorById = getDoctorById;
            _updateDoctor = updateDoctor;
        }

        /// <summary>
        /// Process delete doctor request.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>
        /// An <see cref="ApiResponse{DeleteDoctorResponse}"/> indicating the result of the operation.
        /// </returns>
        public async Task<ApiResponse<DeleteDoctorResponse>> Process(Guid id)
        {
            // 1. Initialize validation flag
            bool isDoctorFound = true;
            // 2. Retrieve doctor by id
            var doctor = await RetrieveDoctor(id);
            // 3. Validate retrieved doctor
            ValidateDoctor(doctor, ref isDoctorFound);
            // 4. Create response
            return await CreateResponse(doctor, isDoctorFound);
        }

        /// <summary>
        /// Retrieve doctor from database.
        /// </summary>
        /// <param name="id">The doctor id.</param>
        /// <returns>
        /// A <see cref="Doctor"/> entity if found; otherwise null.
        /// </returns>
        private async Task<Doctor> RetrieveDoctor(Guid id)
        {
            return await _getDoctorById.Execute(id);
        }

        /// <summary>
        /// Validate retrieved doctor.
        /// </summary>
        /// <param name="doctor">The doctor entity.</param>
        /// <param name="isDoctorFound">
        /// A flag indicating whether the doctor exists.
        /// </param>
        private void ValidateDoctor(Doctor doctor, ref bool isDoctorFound)
        {
            // Check if doctor exists
            if (doctor == null)
            {
                isDoctorFound = false;
            }
        }

        /// <summary>
        /// Create response after processing delete doctor.
        /// </summary>
        /// <param name="doctor">The doctor entity.</param>
        /// <param name="isDoctorFound">
        /// Indicates whether the doctor exists.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{DeleteDoctorResponse}"/> representing the result of the delete operation.
        /// </returns>
        private async Task<ApiResponse<DeleteDoctorResponse>> CreateResponse(
            Doctor doctor,
            bool isDoctorFound)
        {
            // Doctor not found
            if (!isDoctorFound)
            {
                return ApiResponse<DeleteDoctorResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }
            // Soft delete doctor
            doctor.IsDeleted = true;
            doctor.DeletedAt = DateTimeOffset.UtcNow;
            doctor.DeletedBy = "system";
            doctor.LastModifiedBy = "system";
            doctor.LastModifiedDate = DateTimeOffset.UtcNow;
            // Update doctor in database
            await _updateDoctor.Execute(doctor);
            // Create response object
            var response = new DeleteDoctorResponse
            {
                IsDeleted = true
            };
            // Return success response
            return ApiResponse<DeleteDoctorResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response
            );
        }
    }
}