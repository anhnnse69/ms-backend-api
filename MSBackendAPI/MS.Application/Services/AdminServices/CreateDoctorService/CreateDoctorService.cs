using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.CreateDoctor;

namespace MS.Application.Services.AdminServices.CreateDoctorService
{
    /// <summary>
    /// Provides an implementation of the ICreateDoctorService interface
    /// for handling doctor creation logic.
    /// </summary>
    public class CreateDoctorService : ICreateDoctorService
    {
        private readonly ICreateDoctor _createDoctor;

        /// <summary>
        /// Initializes a new instance of the CreateDoctorService class.
        /// </summary>
        /// <param name="createDoctor">
        /// Repository responsible for persisting new doctor data.
        /// </param>
        public CreateDoctorService(ICreateDoctor createDoctor)
        {
            _createDoctor = createDoctor;
        }

        /// <summary>
        /// Processes the create doctor request.
        /// </summary>
        /// <param name="request">
        /// The request containing the information needed to create a new doctor.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the result of the operation
        /// and the ID of the newly created doctor if successful.
        /// </returns>
        public async Task<ApiResponse<Guid>> Process(CreateDoctorRequest request)
        {
            var doctor = BuildDoctorEntity(request);
            return await CreateResponse(doctor);
        }

        /// <summary>
        /// Creates the final API response after processing.
        /// </summary>
        /// <param name="doctor">
        /// The doctor entity to be persisted.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> representing the result of the operation.
        /// </returns>
        private async Task<ApiResponse<Guid>> CreateResponse(Doctor doctor)
        {
            await _createDoctor.Execute(doctor);
            return ApiResponse<Guid>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                doctor.Id
            );
        }

        /// <summary>
        /// Builds a new <see cref="Doctor"/> entity from the request data.
        /// </summary>
        /// <param name="request">
        /// The create doctor request containing doctor information.
        /// </param>
        /// <returns>
        /// A newly constructed <see cref="Doctor"/> entity ready to be persisted.
        /// </returns>
        private Doctor BuildDoctorEntity(CreateDoctorRequest request)
        {
            return new Doctor
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                FullName = request.FullName,
                DisplayName = request.FullName,
                Email = request.Email.ToLower(),
                PhoneNumber = request.PhoneNumber,
                AvatarUrl = request.AvatarUrl,
                PhotoUrl = request.PhotoUrl,
                BioVi = request.BioVi,
                BioEn = request.BioEn,
                AcademicTitleVi = request.AcademicTitleVi,
                AcademicTitleEn = request.AcademicTitleEn,
                YearsOfExperience = request.YearsOfExperience,
                SpecialtyId = request.SpecialtyId,
                AverageRating = 0,
                RatingCount = 0,
                IsDeleted = false,
                CreateBy = "system",
                LastModifiedBy = "system",
                CreateDate = DateTime.UtcNow
            };
        }
    }
}