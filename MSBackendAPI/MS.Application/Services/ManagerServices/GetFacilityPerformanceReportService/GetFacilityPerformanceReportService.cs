using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentsForReportAsync;
using MS.Infrastructure.Repositories.ManagerRepositories.GetFacilityIdByManagerId;

namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Service implementation for retrieving facility performance report.
    /// </summary>
    public class GetFacilityPerformanceReportService : IGetFacilityPerformanceReportService
    {
        private readonly IGetFacilityPerformanceReport _appointmentReportRepository;
        private readonly IGetFacilityIdByManagerId _managerFacilityLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityPerformanceReportService"/> class.
        /// </summary>
        /// <param name="appointmentReportRepository">Repository for retrieving appointment report data.</param>
        /// <param name="managerFacilityLookup">Repository for looking up the facility assigned to a manager.</param>
        public GetFacilityPerformanceReportService(
            IGetFacilityPerformanceReport appointmentReportRepository,
            IGetFacilityIdByManagerId managerFacilityLookup)
        {
            _appointmentReportRepository = appointmentReportRepository;
            _managerFacilityLookup = managerFacilityLookup;
        }

        /// <summary>
        /// Processes the request to retrieve facility performance report.
        /// </summary>
        /// <param name="request">The request containing filter criteria.</param>
        /// <param name="managerId">The unique identifier of the manager making the request.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing <see cref="GetFacilityPerformanceReportResponse"/>
        /// with overall and per-doctor performance metrics.
        /// </returns>
        public async Task<ApiResponse<GetFacilityPerformanceReportResponse>> Process(GetFacilityPerformanceReportRequest request, Guid managerId)
        {
            // 1. Initialize validation flags
            bool isDateRangeValid = true;
            bool isManagerFacilityValid = true;
            // 2. Validate date range
            ValidateDateRange(request.StartDate, request.EndDate, ref isDateRangeValid);
            // 3. Retrieve manager facility ID
            Guid? managerFacilityId = await RetrieveManagerFacilityId(managerId);
            // 4. Validate manager facility access
            ValidateManagerFacility(managerFacilityId, request.FacilityId ?? Guid.Empty, ref isManagerFacilityValid);
            // 5. Retrieve appointments
            Guid resolvedFacilityId = isManagerFacilityValid ? managerFacilityId!.Value : Guid.Empty;
            var appointments = await RetrieveAppointments(resolvedFacilityId, request.StartDate, request.EndDate, request.DoctorId);
            // 6. Calculate overall metrics
            var overallMetrics = CalculateOverallMetrics(appointments, isDateRangeValid && isManagerFacilityValid);
            // 7. Calculate per-doctor performances
            var doctorPerformances = CalculateDoctorPerformances(appointments, isDateRangeValid && isManagerFacilityValid);
            // 8. Map to response
            var response = MapToResponse(overallMetrics, doctorPerformances);
            // 9. Return API response
            return CreateResponse(response, isDateRangeValid, isManagerFacilityValid);
        }

        /// <summary>
        /// Retrieves the facility identifier associated with the specified manager.
        /// </summary>
        /// <param name="managerId">The unique identifier of the manager.</param>
        /// <returns>The facility identifier if found; otherwise null.</returns>
        private async Task<Guid?> RetrieveManagerFacilityId(Guid managerId)
        {
            return await _managerFacilityLookup.Execute(managerId);
        }

        /// <summary>
        /// Validates that the manager has an assigned facility and that it matches the requested facility identifier.
        /// </summary>
        /// <param name="managerFacilityId">The facility identifier retrieved from the manager's profile.</param>
        /// <param name="requestedFacilityId">Facility identifier from the request for cross-validation; <see cref="Guid.Empty"/> if not provided.</param>
        /// <param name="isManagerFacilityValid">Validation flag; set to false if the manager has no facility or the identifiers do not match.</param>
        private void ValidateManagerFacility(Guid? managerFacilityId, Guid requestedFacilityId, ref bool isManagerFacilityValid)
        {
            if (!managerFacilityId.HasValue)
            {
                isManagerFacilityValid = false;
                return;
            }
            if (requestedFacilityId != Guid.Empty && requestedFacilityId != managerFacilityId.Value)
            {
                isManagerFacilityValid = false;
            }
        }

        /// <summary>
        /// Retrieves appointments from the repository based on the specified filters.
        /// </summary>
        /// <param name="facilityId">The facility identifier to filter appointments.</param>
        /// <param name="startDate">Optional start date filter (inclusive).</param>
        /// <param name="endDate">Optional end date filter (inclusive).</param>
        /// <param name="doctorId">Optional doctor identifier filter.</param>
        /// <returns>List of appointments matching the specified criteria.</returns>
        private async Task<List<Appointment>> RetrieveAppointments(Guid facilityId, DateTime? startDate, DateTime? endDate, Guid? doctorId)
        {
            return await _appointmentReportRepository.Execute(facilityId, startDate, endDate, doctorId);
        }

        /// <summary>
        /// Validates that the start date is not greater than the end date when both are provided.
        /// </summary>
        /// <param name="startDate">Optional start date to validate.</param>
        /// <param name="endDate">Optional end date to validate.</param>
        /// <param name="isDateRangeValid">Validation flag; set to false if start date is greater than end date.</param>
        private void ValidateDateRange(DateTime? startDate, DateTime? endDate, ref bool isDateRangeValid)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                isDateRangeValid = false;
            }
        }

        /// <summary>
        /// Calculates overall appointment metrics from the provided appointment list.
        /// </summary>
        /// <param name="appointments">List of appointments to compute metrics from.</param>
        /// <param name="isValid">Flag indicating whether the data is valid for calculation.</param>
        /// <returns>A tuple containing total, completed, cancelled counts and completion rate; all zero if invalid.</returns>
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
        /// Calculates per-doctor performance metrics grouped by doctor identifier.
        /// </summary>
        /// <param name="appointments">List of appointments to group and compute metrics from.</param>
        /// <param name="isValid">Flag indicating whether the data is valid for calculation.</param>
        /// <returns>List of <see cref="DoctorPerformanceResponse"/> with per-doctor metrics; empty list if invalid.</returns>
        private List<DoctorPerformanceResponse> CalculateDoctorPerformances(
            List<Appointment> appointments,
            bool isValid)
        {
            if (!isValid || appointments == null || appointments.Count == 0)
            {
                return new List<DoctorPerformanceResponse>();
            }
            return appointments
                .Where(a => a.DoctorId != null)
                .GroupBy(a => a.DoctorId!.Value)
                .Select(g => new DoctorPerformanceResponse
                {
                    DoctorId = g.Key,
                    DoctorName = g.First().Doctor?.FullName ?? "Unknown",
                    TotalAppointments = g.Count(),
                    CompletedAppointments = g.Count(a => a.Status == AppointmentStatus.Completed),
                    CancelledAppointments = g.Count(a => a.Status == AppointmentStatus.Cancelled),
                    CompletionRate = CalculateCompletionRate(
                        g.Count(a => a.Status == AppointmentStatus.Completed),
                        g.Count())
                })
                .ToList();
        }

        /// <summary>
        /// Calculates the completion rate percentage from completed and total counts.
        /// </summary>
        /// <param name="completed">Number of completed appointments.</param>
        /// <param name="total">Total number of appointments.</param>
        /// <returns>Completion rate rounded to 2 decimal places; 0 if total is zero.</returns>
        private double CalculateCompletionRate(int completed, int total)
        {
            if (total == 0)
            {
                return 0;
            }
            return Math.Round((double)completed / total * 100, 2);
        }

        /// <summary>
        /// Maps computed overall metrics and doctor performances to <see cref="GetFacilityPerformanceReportResponse"/>.
        /// </summary>
        /// <param name="overall">Pre-calculated overall metrics tuple to populate in the response.</param>
        /// <param name="doctorPerformances">Pre-calculated per-doctor performance list to populate in the response.</param>
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
                DoctorPerformances = doctorPerformances
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isDateRangeValid">Flag indicating whether the date range is valid.</param>
        /// <param name="isManagerFacilityValid">Flag indicating whether the manager facility access is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<GetFacilityPerformanceReportResponse> CreateResponse(
            GetFacilityPerformanceReportResponse response,
            bool isDateRangeValid,
            bool isManagerFacilityValid)
        {
            if (!isDateRangeValid)
            {
                return ApiResponse<GetFacilityPerformanceReportResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            }
            if (!isManagerFacilityValid)
            {
                return ApiResponse<GetFacilityPerformanceReportResponse>.Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            return ApiResponse<GetFacilityPerformanceReportResponse>.Success(MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}