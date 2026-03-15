using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentReport;

namespace MS.Application.Services.ManagerServices.AppointmentReportService
{
    /// <summary>
    /// Service responsible for generating appointment report statistics
    /// </summary>
    public class GetAppointmentReportService : IGetAppointmentReportService
    {
        private readonly IGetAppointmentReport _repository;
        /// <summary>
        /// Initializes a new instance of the service
        /// </summary>
        /// <param name="repository">
        /// Repository used to retrieve appointment data
        /// </param>
        public GetAppointmentReportService(IGetAppointmentReport repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Process request to generate appointment report
        /// </summary>
        /// <param name="request">
        /// Report request containing facility identifier and report type
        /// </param>
        /// <returns>Appointment statistics report</returns>
        public async Task<ApiResponse<AppointmentReportResponse>> Process(AppointmentReportRequest request)
        {
            // 1. Initialize validation flag
            bool isDataValid = true;
            // 2. Build filtered query based on request parameters
            var query = BuildQuery(request).AsNoTracking();
            // 3. Retrieve appointments
            var appointments = await query.ToListAsync();
            // 4. Validate retrieved data
            ValidateRetrievedData(appointments, ref isDataValid);
            // 5. Create API response
            return CreateResponse(appointments, isDataValid);
        }

        /// <summary>
        /// Build appointment query with report filtering conditions
        /// </summary>
        /// <param name="request">Appointment report request</param>
        /// <returns>IQueryable appointment query</returns>
        private IQueryable<Appointment> BuildQuery(AppointmentReportRequest request)
        {
            var query = _repository.Execute();
            query = query.Where(x => x.FacilityId == request.FacilityId);
            var date = request.Date ?? DateTime.UtcNow;
            switch (request.Type?.ToLower())
            {
                case "month":
                    query = query.Where(x =>
                        x.AppointmentTime.Year == date.Year &&
                        x.AppointmentTime.Month == date.Month);
                    break;
                case "year":
                    query = query.Where(x =>
                        x.AppointmentTime.Year == date.Year);
                    break;
                default:
                    query = query.Where(x =>
                        x.AppointmentTime.Date == date.Date);
                    break;
            }
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
        /// Create API response for appointment report
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response containing report statistics</returns>
        private ApiResponse<AppointmentReportResponse> CreateResponse(
            List<Appointment> appointments,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<AppointmentReportResponse>.Fail(
                    MessageCode.APP_MESSAGE_4012.ToString());
            }
            // Map appointment data to report response
            var report = MapReport(appointments);
            return ApiResponse<AppointmentReportResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                report);
        }

        /// <summary>
        /// Map appointment entities to report statistics
        /// </summary>
        /// <param name="appointments">List of appointment entities</param>
        /// <returns>Appointment report response</returns>
        private AppointmentReportResponse MapReport(List<Appointment> appointments)
        {
            return new AppointmentReportResponse
            {
                Pending = appointments.Count(x => x.Status == AppointmentStatus.PendingConfirmation),
                Confirmed = appointments.Count(x => x.Status == AppointmentStatus.Confirmed),
                CheckedIn = appointments.Count(x => x.Status == AppointmentStatus.CheckedIn),
                InProgress = appointments.Count(x => x.Status == AppointmentStatus.InProgress),
                Completed = appointments.Count(x => x.Status == AppointmentStatus.Completed),
                Cancelled = appointments.Count(x => x.Status == AppointmentStatus.Cancelled),
                NoShow = appointments.Count(x => x.Status == AppointmentStatus.NoShow),
                Total = appointments.Count
            };
        }
    }
}