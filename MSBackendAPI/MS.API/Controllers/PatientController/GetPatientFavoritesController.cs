using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.GetFavoritesService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Handles requests for retrieving the patient's paginated favorites list.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/favorites")]
    [Authorize(Roles = "Patient")]
    public class GetPatientFavoritesController : ControllerBase
    {
        private readonly IGetFavoritesService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientFavoritesController"/> class.
        /// </summary>
        /// <param name="service">Service responsible for get favorites business logic.</param>
        public GetPatientFavoritesController(IGetFavoritesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the authenticated patient's paginated favorites list with optional type filter.
        /// </summary>
        /// <param name="request">Query parameters containing page, size, and type filter.</param>
        /// <returns>
        /// Returns a paginated list of favorites including doctor or facility information.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetFavorites([FromQuery] GetFavoritesRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }
            var result = await _service.Process(request, userId);
            return Ok(result);
        }
    }
}
