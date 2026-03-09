using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.CreateFacility;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByEmail;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByName;
using MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.GetFacilityByPhone;

namespace MS.Application.Services.AdminServices.FaclityService.CreateFacilityService
{
    /// <summary>
    /// Create facility service implementation
    /// </summary>
    public class CreateFacilityService : ICreateFacilityService
    {
        private readonly ICreateFacility _createFacility;
        private readonly IGetFacilityByEmail _getFacilityByEmail;
        private readonly IGetFacilityByPhone _getFacilityByPhone;
        private readonly IGetFacilityByName _getFacilityByName;

        /// <summary>
        /// Initializes a new instance of the CreateFacilityService class.
        /// </summary>
        /// <param name="createFacility">Repository responsible for creating a facility.</param>
        /// <param name="getFacilityByEmail">Repository used to retrieve facility by email.</param>
        /// <param name="getFacilityByPhone">Repository used to retrieve facility by phone.</param>
        /// <param name="getFacilityByName">Repository used to retrieve facility by name.</param>
        public CreateFacilityService(
            ICreateFacility createFacility,
            IGetFacilityByEmail getFacilityByEmail,
            IGetFacilityByPhone getFacilityByPhone,
            IGetFacilityByName getFacilityByName)
        {
            _createFacility = createFacility;
            _getFacilityByEmail = getFacilityByEmail;
            _getFacilityByPhone = getFacilityByPhone;
            _getFacilityByName = getFacilityByName;
        }

        /// <summary>
        /// Process create facility request
        /// </summary>
        /// <param name="request">Facility creation request model.</param>
        /// <returns></returns>
        public async Task<ApiResponse<Guid>> Process(CreateFacilityRequest request)
        {
            // 1. Initialize validation flags
            bool isEmailExists = false;
            bool isPhoneExists = false;
            bool isNameExists = false;
            // 2. Retrieve existing facilities by unique fields
            var emailFacility = await RetrieveFacilityByEmail(request.Email);
            var phoneFacility = await RetrieveFacilityByPhone(request.Phone);
            var nameFacility = await RetrieveFacilityByName(request.NameVi);
            // 3. Validate retrieved data
            ValidateFacilityData(emailFacility, phoneFacility, nameFacility, ref isEmailExists, ref isPhoneExists, ref isNameExists);
            // 4. Create response
            return await CreateResponse(request, isEmailExists, isPhoneExists, isNameExists);
        }

        /// <summary>
        /// Retrieve facility by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<Facility> RetrieveFacilityByEmail(string email)
        {
            return await _getFacilityByEmail.Execute(email.ToLower().Trim());
        }

        /// <summary>
        /// Retrieve facility by phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private async Task<Facility> RetrieveFacilityByPhone(string phone)
        {
            return await _getFacilityByPhone.Execute(phone.Trim());
        }

        /// <summary>
        /// Retrieve facility by Vietnamese name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<Facility> RetrieveFacilityByName(string name)
        {
            return await _getFacilityByName.Execute(name.Trim());
        }

        /// <summary>
        /// Validate facility data to check for duplicated values
        /// </summary>
        /// <param name="emailFacility"></param>
        /// <param name="phoneFacility"></param>
        /// <param name="nameFacility"></param>
        /// <param name="isEmailExists"></param>
        /// <param name="isPhoneExists"></param>
        /// <param name="isNameExists"></param>
        private void ValidateFacilityData( Facility emailFacility, Facility phoneFacility, Facility nameFacility, ref bool isEmailExists, ref bool isPhoneExists, ref bool isNameExists)
        {
            if (emailFacility != null)
                isEmailExists = true;

            if (phoneFacility != null)
                isPhoneExists = true;

            if (nameFacility != null)
                isNameExists = true;
        }

        /// <summary>
        /// Create response for facility creation request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isEmailExists"></param>
        /// <param name="isPhoneExists"></param>
        /// <param name="isNameExists"></param>
        /// <returns></returns>
        private async Task<ApiResponse<Guid>> CreateResponse( CreateFacilityRequest request, bool isEmailExists, bool isPhoneExists, bool isNameExists)
        {
            if (isEmailExists)
            {
                return ApiResponse<Guid>.Fail(
                    MessageCode.APP_MESSAGE_4017.ToString()
                );
            }
            if (isPhoneExists)
            {
                return ApiResponse<Guid>.Fail(
                    MessageCode.APP_MESSAGE_4018.ToString()
                );
            }
            if (isNameExists)
            {
                return ApiResponse<Guid>.Fail(
                    MessageCode.APP_MESSAGE_4019.ToString()
                );
            }
            // Build facility entity
            var facility = BuildFacilityEntity(request);
            // Persist facility into database
            await _createFacility.Execute(facility);
            // Return success response with created facility id
            return ApiResponse<Guid>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                facility.Id
            );
        }

        /// <summary>
        /// Build facility entity from request model
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Facility BuildFacilityEntity(CreateFacilityRequest request)
        {
            return new Facility
            {
                Id = Guid.NewGuid(),
                NameVi = request.NameVi,
                NameEn = request.NameEn,
                DescriptionVi = request.DescriptionVi,
                DescriptionEn = request.DescriptionEn,
                LogoUrl = request.LogoUrl,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email.ToLower(),
                City = request.City,
                Type = request.Type,
                IsActive = true,
                CreateBy = "system",
                LastModifiedBy = "system",
                CreateDate = DateTime.UtcNow,
                LastModifiedDate = DateTimeOffset.UtcNow
            };
        }
    }
}