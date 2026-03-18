using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.SearchDoctorService;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller responsible for handling patient requests to search for doctors.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/doctors")]
    [Authorize(Roles = "Patient")]
    public class SearchPatientDoctorController : ControllerBase
    {
        private readonly ISearchDoctorService _searchDoctorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPatientDoctorController"/> class.
        /// </summary>
        /// <param name="searchDoctorService">Service responsible for doctor search logic.</param>
        public SearchPatientDoctorController(ISearchDoctorService searchDoctorService)
        {
            _searchDoctorService = searchDoctorService;
        }

        /// <summary>
        /// Searches for doctors by keyword, specialty, facility, location, and pagination parameters.
        /// </summary>
        /// <param name="keyword">Optional keyword to search by doctor name or facility name.</param>
        /// <param name="specialtyId">Optional specialty identifier to filter results.</param>
        /// <param name="facilityId">Optional facility identifier to filter results.</param>
        /// <param name="location">Optional city or address to filter by location.</param>
        /// <param name="page">Current page index (default = 1).</param>
        /// <param name="size">Number of records per page (default = 10).</param>
        /// <returns>
        /// Returns a paginated list of doctors matching the search criteria.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> SearchDoctors(
            [FromQuery] string? keyword,
            [FromQuery] Guid? specialtyId,
            [FromQuery] Guid? facilityId,
            [FromQuery] string? location,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _searchDoctorService.Process(keyword, specialtyId, facilityId, location, page, size);
            return Ok(result);
        }
    }
}
