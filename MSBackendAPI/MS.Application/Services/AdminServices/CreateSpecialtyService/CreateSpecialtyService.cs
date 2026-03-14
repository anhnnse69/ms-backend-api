using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.CreateSpecialty;
using MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.GetSpecialtyByName;

namespace MS.Application.Services.AdminServices.CreateSpecialtyService
{
    /// <summary>
    /// Provides an implementation of the ICreateSpecialtyService interface
    /// for handling specialty creation logic.
    /// </summary>
    public class CreateSpecialtyService : ICreateSpecialtyService
    {
        private readonly ICreateSpecialty _createSpecialty;
        private readonly IGetSpecialtyByName _getSpecialtyByName;

        /// <summary>
        /// Initializes a new instance of the CreateSpecialtyService class.
        /// </summary>
        /// <param name="createSpecialty">
        /// Repository responsible for persisting new specialty data.
        /// </param>
        /// <param name="getSpecialtyByName">
        /// Repository used to retrieve existing specialties by Vietnamese name.
        /// </param>
        public CreateSpecialtyService(
            ICreateSpecialty createSpecialty,
            IGetSpecialtyByName getSpecialtyByName)
        {
            _createSpecialty = createSpecialty;
            _getSpecialtyByName = getSpecialtyByName;
        }

        /// <summary>
        /// Processes the create specialty request.
        /// </summary>
        /// <param name="request">
        /// The request containing the information needed to create a new specialty.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> containing the result of the operation
        /// and the ID of the newly created specialty if successful.
        /// </returns>
        public async Task<ApiResponse<Guid>> Process(CreateSpecialtyRequest request)
        {
            // 1. Initialize validation flag
            bool isNameExists = false;
            // 2. Retrieve specialty by Vietnamese name
            var nameSpecialty = await RetrieveSpecialtyData(request.NameVi.Trim());
            // 3. Validate retrieved data
            ValidateSpecialtyData(nameSpecialty, ref isNameExists);
            // 4. Create response
            return await CreateResponse(request, isNameExists);
        }

        /// <summary>
        /// Retrieves an existing specialty by Vietnamese name.
        /// </summary>
        /// <param name="name">
        /// The Vietnamese name used to search for an existing specialty.
        /// </param>
        /// <returns>
        /// A <see cref="Specialty"/> entity if found; otherwise null.
        /// </returns>
        private async Task<Specialty?> RetrieveSpecialtyData(string name)
        {
            return await _getSpecialtyByName.Execute(name);
        }

        /// <summary>
        /// Validates whether the provided specialty already exists.
        /// </summary>
        /// <param name="existingSpecialty">
        /// The existing specialty retrieved from the database.
        /// </param>
        /// <param name="isNameAlreadyExists">
        /// A flag indicating whether the specialty name already exists.
        /// </param>
        private void ValidateSpecialtyData(Specialty? existingSpecialty, ref bool isNameAlreadyExists)
        {
            if (existingSpecialty != null)
            {
                isNameAlreadyExists = true;
            }
        }

        /// <summary>
        /// Creates the final API response after validation.
        /// </summary>
        /// <param name="request">
        /// The create specialty request.
        /// </param>
        /// <param name="isNameAlreadyExists">
        /// Indicates whether the specialty name already exists in the system.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{Guid}"/> representing the result of the operation.
        /// </returns>
        private async Task<ApiResponse<Guid>> CreateResponse(CreateSpecialtyRequest request, bool isNameAlreadyExists)
        {
            if (isNameAlreadyExists)
            {
                return ApiResponse<Guid>.Fail(
                    MessageCode.APP_MESSAGE_4023.ToString()
                );
            }
            var newSpecialty = BuildSpecialtyEntity(request);
            await _createSpecialty.Execute(newSpecialty);
            return ApiResponse<Guid>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                newSpecialty.Id
            );
        }

        /// <summary>
        /// Builds a new <see cref="Specialty"/> entity from the request data.
        /// </summary>
        /// <param name="request">
        /// The create specialty request containing specialty information.
        /// </param>
        /// <returns>
        /// A newly constructed <see cref="Specialty"/> entity ready to be persisted.
        /// </returns>
        private Specialty BuildSpecialtyEntity(CreateSpecialtyRequest request)
        {
            return new Specialty
            {
                Id = Guid.NewGuid(),
                NameVi = request.NameVi,
                NameEn = request.NameEn,
                DescriptionVi = request.DescriptionVi,
                DescriptionEn = request.DescriptionEn,
                IconUrl = request.IconUrl,
                IsDeleted = false,
                DeletedAt = null,
                DeletedBy = null,
                CreateBy = "system",
                LastModifiedBy = "system",
                CreateDate = DateTime.UtcNow,
                LastModifiedDate = DateTimeOffset.UtcNow
            };
        }
    }
}