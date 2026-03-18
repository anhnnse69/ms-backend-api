using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.AdminServices.DeleteDoctorService;

namespace MS.API.Controllers.AdminController
{
    /// <summary>
    /// Defines API endpoints for administrative operations related to deleting doctors.
    /// </summary>
    /// <remarks>
    /// This controller allows administrators to delete doctor profiles (soft delete).
    /// All routes are prefixed with 'api/v1/admin/doctors' and require the ITAdmin role.
    /// </remarks>
    [Route("api/v1/admin/doctors")]
    [ApiController]
    [Authorize(Roles = "ITAdmin")]
    public class AdminDeleteDoctorController : ControllerBase
    {
        private readonly IDeleteDoctorService _service;

        /// <summary>
        /// Initializes a new instance of the AdminDeleteDoctorController class.
        /// </summary>
        /// <param name="service">
        /// The service responsible for handling doctor delete operations.
        /// </param>
        public AdminDeleteDoctorController(IDeleteDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Deletes an existing doctor in the system (soft delete).
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the doctor to delete.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the delete operation.
        /// Returns a 200 OK response with deletion status if successful.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Process(id);
            return Ok(result);
        }
    }
}