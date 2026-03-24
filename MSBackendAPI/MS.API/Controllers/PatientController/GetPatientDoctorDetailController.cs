using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.PatientServices.GetDoctorDetailService;

namespace MS.API.Controllers.PatientController
{
    /// <summary>
    /// Controller responsible for handling requests to view detailed doctor profile information.
    /// </summary>
    [ApiController]
    [Route("api/v1/patient/doctors")]
    [AllowAnonymous]
    public class GetPatientDoctorDetailController : ControllerBase
    {
        private readonly IGetDoctorDetailService _getDoctorDetailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientDoctorDetailController"/> class.
        /// </summary>
        /// <param name="getDoctorDetailService">Service responsible for doctor detail retrieval logic.</param>
        public GetPatientDoctorDetailController(IGetDoctorDetailService getDoctorDetailService)
        {
            _getDoctorDetailService = getDoctorDetailService;
        }

        /// <summary>
        /// Retrieves the detailed profile, specialty, facilities, and availability schedule of a doctor.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to retrieve.</param>
        /// <returns>
        /// Returns the doctor's full profile including specialty, facilities, and weekly schedule.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorDetail([FromRoute] Guid id)
        {
            var result = await _getDoctorDetailService.Process(id);
            return Ok(result);
        }
    }
}
