using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByFacility;

namespace MS.Application.Services.ManagerServices.AppointmentByFacilityService
{
    /// <summary>
    /// Service responsible for retrieving appointments by facility
    /// </summary>
    public class GetAppointmentByFacilityService : IGetAppointmentByFacilityService
    {
        private readonly IGetAppointmentByFacility _repository;

        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="repository">
        /// Repository used to retrieve appointments by facility identifier
        /// </param>
        public GetAppointmentByFacilityService(IGetAppointmentByFacility repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Process request to retrieve appointments
        /// </summary>
        /// <param name="request">
        /// Appointment filter request containing facility identifier,
        /// pagination information, and optional filtering conditions
        /// </param>
        /// <returns>Paginated list of appointments</returns>
        public async Task<ApiResponse<List<AppointmentResponse>>> Process(AppointmentFilterRequest request)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Build filtered query based on request parameters
            var query = BuildQuery(request).AsNoTracking();
            // 3. Get total number of records matching the filter
            var total = await query.CountAsync();
            // 4. Retrieve paginated appointments ordered by appointment time (descending)
            var appointments = await query
                .OrderByDescending(x => x.AppointmentTime)
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync();
            // 5. Validate retrieved data
            ValidateRetrievedData(appointments, ref isDataValid);
            // 6. Create API response
            return CreateResponse(appointments, request.Page, request.Size, total, isDataValid);
        }

        /// <summary>
        /// Build appointment query with optional filtering conditions
        /// </summary>
        /// <param name="request">Appointment filter request</param>
        /// <returns>IQueryable appointment query</returns>
        private IQueryable<Appointment> BuildQuery(AppointmentFilterRequest request)
        {
            var query = _repository.Execute(request.FacilityId);
            if (request.DoctorId.HasValue)
                query = query.Where(x => x.DoctorId == request.DoctorId.Value);
            if (request.Status.HasValue)
                query = query.Where(x => x.Status == request.Status.Value);
            if (request.Date.HasValue)
                query = query.Where(x => x.AppointmentTime.Date == request.Date.Value.Date);
            return query;
        }

        /// <summary>
        /// Validate retrieved appointment data
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateRetrievedData(List<Appointment> appointments, ref bool isDataValid)
        {
            if (appointments == null)
                isDataValid = false;
        }

        /// <summary>
        /// Create API response from appointment entities
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <param name="total">Total number of records</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing appointment data</returns>
        private ApiResponse<List<AppointmentResponse>> CreateResponse(
            List<Appointment> appointments,
            int page,
            int size,
            int total,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<List<AppointmentResponse>>.Fail(
                    MessageCode.APP_MESSAGE_4012.ToString());
            }
            // Map appointment entities to response model
            var mappedAppointments = MapAppointments(appointments);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<List<AppointmentResponse>>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                mappedAppointments,
                meta);
        }

        /// <summary>
        /// Map appointment entities to appointment response models
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <returns>List of appointment response models</returns>
        private List<AppointmentResponse> MapAppointments(List<Appointment> appointments)
        {
            return appointments.Select(a => new AppointmentResponse
            {
                Id = a.Id,
                PatientName = a.Patient?.DisplayName,
                DoctorName = a.Doctor?.DisplayName,
                AppointmentTime = a.AppointmentTime,
                Status = (int)a.Status,
                Notes = a.Notes
            }).ToList();
        }
    }
}