using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.Doctors.GetDoctorAppointmentService
{
    /// <summary>
    /// Get doctor appointment service implementation
    /// </summary>
    public class GetDoctorAppointmentService : IGetDoctorAppointmentService
    {
        private readonly IGetDoctorAppointments _getDoctorAppointments;
        private readonly IGetDoctorByUserId _getDoctorByUserId;

        /// <summary>
        /// Get doctor appointment service constructor
        /// </summary>
        /// <param name="getDoctorAppointments"></param>
        /// <param name="getDoctorByUserId"></param>
        public GetDoctorAppointmentService(
            IGetDoctorAppointments getDoctorAppointments,
            IGetDoctorByUserId getDoctorByUserId)
        {
            _getDoctorAppointments = getDoctorAppointments;
            _getDoctorByUserId = getDoctorByUserId;
        }

        /// <summary>
        /// Process get doctor appointment request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>> Process(
            Guid userId,
            int page,
            int size)
        {
            // 1. Initialize validation flags
            bool isRetrievedDataValid = true;
            // 2. Retrieve doctor entity
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment query
            var retrievedQuery = RetrieveQuery(retrievedDoctor);
            // 4. Retrieve paginated data
            var retrievedData = await RetrieveData(retrievedQuery, page, size);
            // 5. Retrieve total records
            var total = await RetrieveTotal(retrievedQuery);
            // 6. Validate retrieved data
            ValidateData(retrievedDoctor, ref isRetrievedDataValid);
            // 7. Create response
            return CreateResponse(retrievedData, total, page, size, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve doctor entity by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve doctor appointment query
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        private IQueryable<Appointment> RetrieveQuery(Doctor doctor)
        {
            if (doctor == null)
            {
                return null;
            }
            return _getDoctorAppointments.Execute(doctor.Id);
        }

        /// <summary>
        /// Retrieve paginated appointment data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private async Task<List<Appointment>> RetrieveData(
            IQueryable<Appointment> query,
            int page,
            int size)
        {
            if (query == null)
            {
                return null;
            }
            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve total number of appointment records
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<int> RetrieveTotal(IQueryable<Appointment> query)
        {
            if (query == null)
            {
                return 0;
            }
            return await query.CountAsync();
        }

        /// <summary>
        /// Validate retrieved doctor data
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="isRetrievedDataValid"></param>
        private void ValidateData(Doctor doctor, ref bool isRetrievedDataValid)
        {
            if (doctor == null)
            {
                isRetrievedDataValid = false;
            }
        }

        /// <summary>
        /// Map appointment entity to response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<GetDoctorAppointmentResponse> MapToResponse(IEnumerable<Appointment> data)
        {
            return data.Select(x => new GetDoctorAppointmentResponse
            {
                // Appointment identifier
                AppointmentId = x.Id,
                // Patient full name
                PatientName = x.Patient?.FullName,
                // Facility name
                FacilityName = x.Facility?.NameVi,
                // Appointment time
                AppointmentTime = x.AppointmentTime,
                // Appointment status
                Status = x.Status,
                // Appointment notes
                Notes = x.Notes
            });
        }

        /// <summary>
        /// Create response of the get doctor appointment request
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="isRetrievedDataValid"></param>
        /// <returns></returns>
        private ApiResponse<IEnumerable<GetDoctorAppointmentResponse>> CreateResponse(
            IEnumerable<Appointment> data,
            int total,
            int page,
            int size,
            bool isRetrievedDataValid)
        {
            if (!isRetrievedDataValid)
            {
                return ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            var result = MapToResponse(data);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<IEnumerable<GetDoctorAppointmentResponse>>
                .Success(
                    MessageCode.APP_MESSAGE_2000.ToString(),
                    result,
                    meta
                );
        }
    }
}