using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Common.Response;
using MS.Application.Services.PatientServices.ChangePasswordService;
using System.Security.Claims;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller for user password operations
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Patient")]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IChangePasswordService _changePasswordService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="changePasswordService"></param>
        public ChangePasswordController(IChangePasswordService changePasswordService)
        {
            _changePasswordService = changePasswordService;
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ChangePasswordResponse>>> ChangePassword
        (
            [FromBody] ChangePasswordRequest request
        )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }
            request.UserId = Guid.Parse(userIdClaim);
            Console.WriteLine(request.UserId);
            var response = await _changePasswordService.Process(request);

            return Ok(response);
        }
    }
}