using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Common.Response;
using MS.Application.Services.ManagerServices.FacilitySpecialtyService;

namespace MS.API.Controllers.CommonController
{
    /// <summary>
    /// Controller handling operations related to Facilities and Specialties
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize(Roles = "Manager")]
    [AllowAnonymous]
    public class FacilitySpecialtyController : ControllerBase
    {
        private readonly IGetFacilitySpecialtiesService _getFacilitySpecialtiesService;

        /// <summary>
        /// Constructor for FacilitySpecialtyController
        /// </summary>
        /// <param name="getFacilitySpecialtiesService">Service for retrieving the list of specialties by facility</param>
        public FacilitySpecialtyController(IGetFacilitySpecialtiesService getFacilitySpecialtiesService)
        {
            _getFacilitySpecialtiesService = getFacilitySpecialtiesService;
        }

        /// <summary>
        /// Gets the list of active specialties at a specific medical facility
        /// </summary>
        /// <param name="facilityId">The ID of the facility to look up</param>
        /// <returns>ApiResponse containing a list of FacilitySpecialtyResponse</returns>
        [HttpGet("facilities/{facilityId}/specialties")]
        [ProducesResponseType(typeof(ApiResponse<List<FacilitySpecialtyResponse>>), 200)]
        public async Task<IActionResult> GetSpecialtiesByFacility([FromRoute] Guid facilityId)
        {
            // Initialize request
            var request = new GetFacilitySpecialtiesRequest
            {
                FacilityId = facilityId
            };
            // Call the process method from the Application layer
            var response = await _getFacilitySpecialtiesService.Process(request);
            // Return the result using the system's standard response format
            return Ok(response);
        }
    }
}