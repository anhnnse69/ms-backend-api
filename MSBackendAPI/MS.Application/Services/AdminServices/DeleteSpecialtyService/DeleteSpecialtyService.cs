using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetSpecialtyById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateSpecialty;

namespace MS.Application.Services.AdminServices.DeleteSpecialtyService
{
    /// <summary>
    /// Soft delete specialty service implementation
    /// </summary>
    public class DeleteSpecialtyService : IDeleteSpecialtyService
    {
        private readonly IGetSpecialtyById _getSpecialtyById;
        private readonly IUpdateSpecialty _updateSpecialty;

        /// <summary>
        /// Initializes a new instance of the DeleteSpecialtyService class.
        /// </summary>
        /// <param name="getSpecialtyById">
        /// Repository used to retrieve specialty by identifier.
        /// </param>
        /// <param name="updateSpecialty">
        /// Repository used to update specialty data.
        /// </param>
        public DeleteSpecialtyService(
            IGetSpecialtyById getSpecialtyById,
            IUpdateSpecialty updateSpecialty)
        {
            _getSpecialtyById = getSpecialtyById;
            _updateSpecialty = updateSpecialty;
        }

        /// <summary>
        /// Process soft delete specialty request
        /// </summary>
        /// <param name="id">The unique identifier of the specialty.</param>
        /// <returns></returns>
        public async Task<ApiResponse<DeleteSpecialtyResponse>> Process(Guid id)
        {
            // 1. Initialize validation flag
            bool isSpecialtyExist = true;
            // 2. Retrieve specialty by id
            var retrievedSpecialty = await RetrieveSpecialtyData(id);
            // 3. Validate retrieved specialty data
            ValidateRetrievedData(retrievedSpecialty, ref isSpecialtyExist);
            // 4. Perform soft delete operation
            await SoftDeleteSpecialty(retrievedSpecialty, isSpecialtyExist);
            // 5. Create response
            return CreateResponse(isSpecialtyExist);
        }

        /// <summary>
        /// Retrieve specialty by id
        /// </summary>
        /// <param name="id">Specialty identifier.</param>
        /// <returns></returns>
        private async Task<Specialty?> RetrieveSpecialtyData(Guid id)
        {
            return await _getSpecialtyById.Execute(id);
        }

        /// <summary>
        /// Validate specialty existence
        /// </summary>
        /// <param name="specialty">Retrieved specialty entity.</param>
        /// <param name="isSpecialtyExist">Flag indicating whether specialty exists.</param>
        private void ValidateRetrievedData(Specialty? specialty, ref bool isSpecialtyExist)
        {
            if (specialty == null)
            {
                isSpecialtyExist = false;
            }
        }

        /// <summary>
        /// Perform soft delete operation for specialty
        /// </summary>
        /// <param name="specialty">Specialty entity.</param>
        /// <param name="isSpecialtyExist">Flag indicating whether specialty exists.</param>
        private async Task SoftDeleteSpecialty(Specialty? specialty, bool isSpecialtyExist)
        {
            if (!isSpecialtyExist)
                return;
            specialty!.IsDeleted = true;
            specialty.DeletedAt = DateTimeOffset.UtcNow;
            specialty.DeletedBy = "system";
            await _updateSpecialty.Execute(specialty);
        }

        /// <summary>
        /// Create response for delete specialty request
        /// </summary>
        /// <param name="isSpecialtyExist">Flag indicating whether specialty exists.</param>
        /// <returns></returns>
        private ApiResponse<DeleteSpecialtyResponse> CreateResponse(bool isSpecialtyExist)
        {
            if (!isSpecialtyExist)
            {
                return ApiResponse<DeleteSpecialtyResponse>.Fail(
                    MessageCode.APP_MESSAGE_4008.ToString()
                );
            }
            return ApiResponse<DeleteSpecialtyResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new DeleteSpecialtyResponse(true)
            );
        }
    }
}