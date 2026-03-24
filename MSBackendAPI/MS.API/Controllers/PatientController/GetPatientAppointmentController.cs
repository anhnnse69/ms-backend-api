using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Common.Response;
using MS.Application.Services.PatientServices.GetPatientAppointment;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller for retrieving patient appointment operations
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class GetPatientAppointmentController : ControllerBase
    {
        private readonly IGetPatientAppointmentsService _getPatientAppointmentsService;

        /// <summary>
        /// Initializes a new instance of the GetPatientAppointmentController with required dependencies
        /// </summary>
        /// <param name="getPatientAppointmentsService">The service to handle retrieving patient appointments</param>
        public GetPatientAppointmentController(IGetPatientAppointmentsService getPatientAppointmentsService)
        {
            _getPatientAppointmentsService = getPatientAppointmentsService;
        }

        /// <summary>
        /// Retrieves the current and past appointments of the currently authenticated patient.
        /// The authenticated user's identifier from the JWT is first resolved to the linked
        /// patient record, then appointments are queried by that patient id.
        /// </summary>
        /// <returns>A list of appointments wrapped in a standardized API response</returns>
        [HttpGet("me/appointments")]
        [ProducesResponseType(typeof(ApiResponse<GetPatientAppointmentsResponse>), 200)]
        [ProducesResponseType(401)] // Unauthorized if token is missing/invalid
        public async Task<ActionResult<ApiResponse<GetPatientAppointmentsResponse>>> GetMyAppointments()
        {
            // Extract the User ID from the JWT Token Claims
            // Note: Update "ClaimTypes.NameIdentifier" if your token uses a custom claim like a custom user id.
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                // Return 401 Unauthorized if the ID cannot be extracted or parsed
                return Unauthorized(ApiResponse<GetPatientAppointmentsResponse>.Fail("APP_MESSAGE_0002"));
            }
            // 1. Initialize the request model for the Application layer
            var request = new GetPatientAppointmentsRequest
            {
                UserId = userId
            };
            // 2. Execute the Process() method of the Service to handle the entire flow
            var response = await _getPatientAppointmentsService.Process(request);
            // 3. Return HTTP 200 OK along with the standardized ApiResponse data
            return Ok(response);
        }
    }
}
