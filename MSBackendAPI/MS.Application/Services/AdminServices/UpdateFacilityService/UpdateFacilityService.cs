using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByEmail;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByPhone;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByName;

namespace MS.Application.Services.AdminServices.UpdateFacilityService
{
    /// <summary>
    /// Provides an implementation of the facility update service.
    /// </summary>
    public class UpdateFacilityService : IUpdateFacilityService
    {
        private readonly IGetFacilityById _getFacilityById;
        private readonly IUpdateFacility _updateFacility;
        private readonly IGetFacilityByEmail _getFacilityByEmail;
        private readonly IGetFacilityByPhone _getFacilityByPhone;
        private readonly IGetFacilityByName _getFacilityByName;

        /// <summary>
        /// Initializes a new instance of the UpdateFacilityService class.
        /// </summary>
        public UpdateFacilityService(
            IGetFacilityById getFacilityById,
            IUpdateFacility updateFacility,
            IGetFacilityByEmail getFacilityByEmail,
            IGetFacilityByPhone getFacilityByPhone,
            IGetFacilityByName getFacilityByName)
        {
            _getFacilityById = getFacilityById;
            _updateFacility = updateFacility;
            _getFacilityByEmail = getFacilityByEmail;
            _getFacilityByPhone = getFacilityByPhone;
            _getFacilityByName = getFacilityByName;
        }

        /// <summary>
        /// Process update facility request
        /// </summary>
        public async Task<ApiResponse<bool>> Process(Guid id, UpdateFacilityRequest request)
        {
            // 1. Initialize validation flags
            bool isFacilityExist = true;
            bool isEmailExists = false;
            bool isPhoneExists = false;
            bool isNameExists = false;
            // 2. Retrieve facility
            var retrievedFacility = await RetrieveFacilityData(id);
            // 3. Retrieve duplicated data
            var emailFacility = await RetrieveFacilityByEmail(request.Email);
            var phoneFacility = await RetrieveFacilityByPhone(request.Phone);
            var nameFacility = await RetrieveFacilityByName(request.NameVi);
            // 4. Validate data
            ValidateRetrievedData(retrievedFacility, ref isFacilityExist);
            ValidateDuplicateData( id, emailFacility, phoneFacility, nameFacility, ref isEmailExists, ref isPhoneExists, ref isNameExists);
            // 5. Update facility
            await UpdateFacilityData(retrievedFacility, request, isFacilityExist, isEmailExists, isPhoneExists, isNameExists);
            // 6. Create response
            return CreateResponse(isFacilityExist, isEmailExists, isPhoneExists, isNameExists);
        }

        /// <summary>
        /// Retrieve facility by id
        /// </summary>
        private async Task<Facility?> RetrieveFacilityData(Guid id)
        {
            return await _getFacilityById.Execute(id);
        }

        /// <summary>
        /// Retrieve facility by email
        /// </summary>
        private async Task<Facility?> RetrieveFacilityByEmail(string email)
        {
            return await _getFacilityByEmail.Execute(email.ToLower().Trim());
        }

        /// <summary>
        /// Retrieve facility by phone
        /// </summary>
        private async Task<Facility?> RetrieveFacilityByPhone(string phone)
        {
            return await _getFacilityByPhone.Execute(phone.Trim());
        }

        /// <summary>
        /// Retrieve facility by name
        /// </summary>
        private async Task<Facility?> RetrieveFacilityByName(string name)
        {
            return await _getFacilityByName.Execute(name.Trim());
        }

        /// <summary>
        /// Validate facility existence
        /// </summary>
        private void ValidateRetrievedData(Facility? facility, ref bool isFacilityExist)
        {
            if (facility == null)
            {
                isFacilityExist = false;
            }
        }

        /// <summary>
        /// Validate duplicated data
        /// </summary>
        private void ValidateDuplicateData( Guid currentId, Facility? emailFacility, Facility? phoneFacility, Facility? nameFacility, ref bool isEmailExists, ref bool isPhoneExists, ref bool isNameExists)
        {
            if (emailFacility != null && emailFacility.Id != currentId)
                isEmailExists = true;
            if (phoneFacility != null && phoneFacility.Id != currentId)
                isPhoneExists = true;
            if (nameFacility != null && nameFacility.Id != currentId)
                isNameExists = true;
        }

        /// <summary>
        /// Update facility data
        /// </summary>
        private async Task UpdateFacilityData(Facility? facility, UpdateFacilityRequest request, bool isFacilityExist, bool isEmailExists, bool isPhoneExists, bool isNameExists)
        {
            if (!isFacilityExist || isEmailExists || isPhoneExists || isNameExists)
                return;
            MapFacilityData(facility!, request);
            await _updateFacility.Execute(facility!);
        }

        /// <summary>
        /// Map request data to facility entity
        /// </summary>
        private void MapFacilityData(Facility facility, UpdateFacilityRequest request)
        {
            facility.NameVi = request.NameVi;
            facility.NameEn = request.NameEn;
            facility.DescriptionVi = request.DescriptionVi;
            facility.DescriptionEn = request.DescriptionEn;
            facility.LogoUrl = request.LogoUrl;
            facility.Address = request.Address;
            facility.Phone = request.Phone;
            facility.Email = request.Email.ToLower();
            facility.City = request.City;
            facility.Type = request.Type;
            facility.LastModifiedBy = "system";
            facility.LastModifiedDate = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Create response
        /// </summary>
        private ApiResponse<bool> CreateResponse(bool isFacilityExist, bool isEmailExists, bool isPhoneExists, bool isNameExists)
        {
            if (!isFacilityExist)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4008.ToString());
            if (isEmailExists)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4017.ToString());
            if (isPhoneExists)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4018.ToString());
            if (isNameExists)
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4023.ToString());
            return ApiResponse<bool>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                true
            );
        }
    }
}