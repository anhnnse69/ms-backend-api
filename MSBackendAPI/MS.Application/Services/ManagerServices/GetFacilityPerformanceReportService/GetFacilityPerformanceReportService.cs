using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentsForReportAsync;

namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Service implementation for retrieving facility performance report.
    /// </summary>
    public class GetFacilityPerformanceReportService : IGetFacilityPerformanceReportService
    {
        private readonly IGetFacilityPerformanceReportRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityPerformanceReportService"/> class.
        /// </summary>
        /// <param name="repository">Repository for retrieving appointment data.</param>
        public GetFacilityPerformanceReportService(IGetFacilityPerformanceReportRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Processes the request to retrieve facility performance report.
        /// </summary>
        /// <param name="request">The request containing filter criteria.</param>
        /// <returns>API response with performance report data.</returns>
        public async Task<ApiResponse<GetFacilityPerformanceReportResponse>> Process(GetFacilityPerformanceReportRequest request)
        {
            // 1. Initialize validation flags
            bool isDateRangeValid = true;
            // 2. Retrieve raw appointments from repository
            var appointments = await RetrieveAppointments(request);
            // 3. Validate input parameters
            ValidateDateRange(request.StartDate, request.EndDate, ref isDateRangeValid);
            // 4. Calculate overall metrics
            var overallMetrics = CalculateOverallMetrics(appointments, isDateRangeValid);
            // 5. Calculate per‑doctor performances
            var doctorPerformances = CalculateDoctorPerformances(appointments, isDateRangeValid);
            // 6. Map to response
            var response = MapToResponse(overallMetrics, doctorPerformances);
            // 7. Return API response
            return CreateResponse(response, isDateRangeValid);
        }

        /// <summary>
        /// Retrieves appointments from the repository based on request filters.
        /// </summary>
        /// <param name="request">The request containing facility, date range, and optional doctor filter.</param>
        /// <returns>List of appointments matching the criteria.</returns>
        private async Task<List<Appointment>> RetrieveAppointments(GetFacilityPerformanceReportRequest request)
        {
            return await _repository.Execute(
                request.FacilityId,
                request.StartDate,
                request.EndDate,
                request.DoctorId);
        }

        /// <summary>
        /// Validates that start date is not greater than end date (if both provided).
        /// </summary>
        /// <param name="startDate">Optional start date.</param>
        /// <param name="endDate">Optional end date.</param>
        /// <param name="isDateRangeValid">Reference flag; set to false if validation fails.</param>
        private void ValidateDateRange(DateTime? startDate, DateTime? endDate, ref bool isDateRangeValid)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                isDateRangeValid = false;
            }
        }

        /// <summary>
        /// Calculates overall metrics (total, completed, cancelled, completion rate) from the appointment list.
        /// </summary>
        /// <param name="appointments">List of appointments.</param>
        /// <param name="isValid">Flag indicating whether input data is valid for calculation.</param>
        /// <returns>Tuple containing overall metrics.</returns>
        private (int Total, int Completed, int Cancelled, double Rate) CalculateOverallMetrics(
            List<Appointment> appointments,
            bool isValid)
        {
            if (!isValid || appointments == null || appointments.Count == 0)
            {
                return (0, 0, 0, 0);
            }
            int total = appointments.Count;
            int completed = appointments.Count(a => a.Status == AppointmentStatus.Completed);
            int cancelled = appointments.Count(a => a.Status == AppointmentStatus.Cancelled);
            double rate = CalculateCompletionRate(completed, total);
            return (total, completed, cancelled, rate);
        }

        /// <summary>
        /// Calculates per‑doctor performance metrics grouped by doctor.
        /// </summary>
        /// <param name="appointments">List of appointments.</param>
        /// <param name="isValid">Flag indicating whether input data is valid for calculation.</param>
        /// <returns>List of doctor performance responses.</returns>
        private List<DoctorPerformanceResponse> CalculateDoctorPerformances(
            List<Appointment> appointments,
            bool isValid)
        {
            if (!isValid || appointments == null || appointments.Count == 0)
            {
                return new List<DoctorPerformanceResponse>();
            }
            var grouped = appointments
                .Where(a => a.DoctorId != null)
                .GroupBy(a => a.DoctorId!.Value)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    DoctorName = g.First().Doctor?.FullName ?? "Unknown",
                    Total = g.Count(),
                    Completed = g.Count(a => a.Status == AppointmentStatus.Completed),
                    Cancelled = g.Count(a => a.Status == AppointmentStatus.Cancelled)
                })
                .ToList();
            var performances = new List<DoctorPerformanceResponse>();
            foreach (var item in grouped)
            {
                performances.Add(new DoctorPerformanceResponse
                {
                    DoctorId = item.DoctorId,
                    DoctorName = item.DoctorName,
                    TotalAppointments = item.Total,
                    CompletedAppointments = item.Completed,
                    CancelledAppointments = item.Cancelled,
                    CompletionRate = CalculateCompletionRate(item.Completed, item.Total)
                });
            }
            return performances;
        }

        /// <summary>
        /// Calculates the completion rate percentage.
        /// </summary>
        /// <param name="completed">Number of completed items.</param>
        /// <param name="total">Total number of items.</param>
        /// <returns>Completion rate rounded to 2 decimal places; 0 if total is zero.</returns>
        private double CalculateCompletionRate(int completed, int total)
        {
            if (total == 0) return 0;
            return Math.Round((double)completed / total * 100, 2);
        }

        /// <summary>
        /// Maps computed metrics to the final response object.
        /// </summary>
        /// <param name="overall">Tuple containing overall metrics.</param>
        /// <param name="doctorPerformances">List of doctor performance responses.</param>
        /// <returns>Mapped <see cref="GetFacilityPerformanceReportResponse"/>.</returns>
        private GetFacilityPerformanceReportResponse MapToResponse(
            (int Total, int Completed, int Cancelled, double Rate) overall,
            List<DoctorPerformanceResponse> doctorPerformances)
        {
            return new GetFacilityPerformanceReportResponse
            {
                TotalAppointments = overall.Total,
                CompletedAppointments = overall.Completed,
                CancelledAppointments = overall.Cancelled,
                CompletionRate = overall.Rate,
                DoctorPerformances = doctorPerformances ?? new List<DoctorPerformanceResponse>()
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isDateRangeValid">Flag indicating whether date range is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<GetFacilityPerformanceReportResponse> CreateResponse(
            GetFacilityPerformanceReportResponse response,
            bool isDateRangeValid)
        {
            if (!isDateRangeValid)
                return ApiResponse<GetFacilityPerformanceReportResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());

            return ApiResponse<GetFacilityPerformanceReportResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                response);
        }
    }
}
