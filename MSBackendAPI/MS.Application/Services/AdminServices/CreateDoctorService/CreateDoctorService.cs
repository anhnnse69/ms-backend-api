using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.CreateDoctor;

namespace MS.Application.Services.AdminServices.CreateDoctorService
{
    /// <summary>
    /// Implements <see cref="ICreateDoctorService"/> to handle doctor creation logic.
    /// </summary>
    public class CreateDoctorService : ICreateDoctorService
    {
        private readonly ICreateDoctor _createDoctor;

        /// <summary>
        /// Initializes a new instance of the service.
        /// </summary>
        /// <param name="createDoctor">
        /// Repository responsible for saving doctor data.
        /// </param>
        public CreateDoctorService(ICreateDoctor createDoctor)
        {
            _createDoctor = createDoctor;
        }

        /// <summary>
        /// Processes the doctor creation request and returns the created doctor ID.
        /// </summary>
        /// <param name="request">Doctor creation request.</param>
        /// <returns>API response containing the new doctor identifier.</returns>
        public async Task<ApiResponse<Guid>> Process(CreateDoctorRequest request)
        {
            var doctor = BuildDoctorEntity(request);
            await _createDoctor.Execute(doctor);
            return ApiResponse<Guid>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                doctor.Id
            );
        }

        /// <summary>
        /// Maps request data to a <see cref="Doctor"/> entity.
        /// </summary>
        /// <param name="request">Doctor creation request.</param>
        /// <returns>Constructed doctor entity.</returns>
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