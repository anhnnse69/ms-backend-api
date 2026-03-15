using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetSpecialtyById;
using MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.GetSpecialtyByName;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateSpecialty;

namespace MS.Application.Services.AdminServices.UpdateSpecialtyService
{
    /// <summary>
    /// Provides an implementation of the specialty update service.
    /// </summary>
    public class UpdateSpecialtyService : IUpdateSpecialtyService
    {
        private readonly IGetSpecialtyById _getSpecialtyById;
        private readonly IGetSpecialtyByName _getSpecialtyByName;
        private readonly IUpdateSpecialty _updateSpecialty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSpecialtyService"/> class.
        /// </summary>
        public UpdateSpecialtyService(
            IGetSpecialtyById getSpecialtyById,
            IGetSpecialtyByName getSpecialtyByName,
            IUpdateSpecialty updateSpecialty)
        {
            _getSpecialtyById = getSpecialtyById;
            _getSpecialtyByName = getSpecialtyByName;
            _updateSpecialty = updateSpecialty;
        }

        /// <summary>
        /// Processes the update specialty request.
        /// </summary>
        public async Task<ApiResponse<bool>> Process(Guid id, UpdateSpecialtyRequest request)
        {
            // 1. Initialize validation flags
            bool isSpecialtyExist = true;
            bool isNameExists = false;
            // 2. Retrieve specialty
            var retrievedSpecialty = await RetrieveSpecialtyData(id);
            // 3. Retrieve duplicated data
            var nameSpecialty = await RetrieveSpecialtyByName(request.NameVi);
            // 4. Validate retrieved data
            ValidateRetrievedData(retrievedSpecialty, ref isSpecialtyExist);
            // 5. Validate duplicated data
            ValidateDuplicateData(id, nameSpecialty, ref isNameExists);
            // 6. Update specialty
            await UpdateSpecialtyData(retrievedSpecialty, request, isSpecialtyExist, isNameExists);
            // 7. Create response
            return CreateResponse(isSpecialtyExist, isNameExists);
        }

        /// <summary>
        /// Retrieves specialty by id.
        /// </summary>
        private async Task<Specialty?> RetrieveSpecialtyData(Guid id)
        {
            return await _getSpecialtyById.Execute(id);
        }

        /// <summary>
        /// Retrieves specialty by name.
        /// </summary>
        private async Task<Specialty?> RetrieveSpecialtyByName(string name)
        {
            return await _getSpecialtyByName.Execute(name.Trim());
        }

        /// <summary>
        /// Validates whether the retrieved specialty exists.
        /// </summary>
        private void ValidateRetrievedData(Specialty? specialty, ref bool isSpecialtyExist)
        {
            if (specialty == null)
            {
                isSpecialtyExist = false;
            }
        }

        /// <summary>
        /// Validates duplicated name.
        /// </summary>
        private void ValidateDuplicateData(Guid currentId, Specialty? nameSpecialty, ref bool isNameExists)
        {
            if (nameSpecialty != null && nameSpecialty.Id != currentId)
                isNameExists = true;
        }

        /// <summary>
        /// Updates specialty data in database.
        /// </summary>
        private async Task UpdateSpecialtyData(
            Specialty? specialty,
            UpdateSpecialtyRequest request,
            bool isSpecialtyExist,
            bool isNameExists)
        {
            if (!isSpecialtyExist || isNameExists)
                return;
            MapSpecialtyData(specialty!, request);
            await _updateSpecialty.Execute(specialty!);
        }

        /// <summary>
        /// Maps request data to specialty entity.
        /// </summary>
        private void MapSpecialtyData(Specialty specialty, UpdateSpecialtyRequest request)
        {
            specialty.NameVi = request.NameVi;
            specialty.NameEn = request.NameEn;
            specialty.DescriptionVi = request.DescriptionVi;
            specialty.DescriptionEn = request.DescriptionEn;
            specialty.IconUrl = request.IconUrl;
            specialty.LastModifiedBy = "system";
            specialty.LastModifiedDate = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Creates API response.
        /// </summary>
        private ApiResponse<bool> CreateResponse(bool isSpecialtyExist, bool isNameExists)
        {
            if (!isSpecialtyExist)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4008.ToString());
            if (isNameExists)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4023.ToString());
            return ApiResponse<bool>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                true
            );
        }
    }
}