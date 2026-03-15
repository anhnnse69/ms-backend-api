using MS.Application.Common.Response;
using MS.Application.Services.PatientServices.GetPatientAppointment;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientAppointment;

/// <summary>
/// Service implementation for retrieving current and past appointments of a patient
/// </summary>
public class GetPatientAppointmentsService : IGetPatientAppointmentsService
{
    private readonly IGetAppointmentsByPatientId _getAppointmentsByPatientId;

    /// <summary>
    /// Constructor for GetPatientAppointmentsService
    /// </summary>
    /// <param name="getAppointmentsByPatientId"></param>
    public GetPatientAppointmentsService(IGetAppointmentsByPatientId getAppointmentsByPatientId)
    {
        _getAppointmentsByPatientId = getAppointmentsByPatientId;
    }

    /// <summary>
    /// Process the request to get patient appointments
    /// </summary>
    /// <param name="request">The request containing patient ID</param>
    /// <returns>API Response with a list of appointment data</returns>
    public async Task<ApiResponse<List<GetPatientAppointmentsResponse>>> Process(GetPatientAppointmentsRequest request)
    {
        // 1. Retrieve appointments data from database
        var appointments = await RetrieveAppointments(request.PatientId);
        // 2. Map domain entities directly to a list of responses
        var responseData = MapToResponse(appointments);
        // 3. Create response
        return CreateResponse(responseData);
    }

    /// <summary>
    /// Retrieve appointments by patient ID
    /// </summary>
    /// <param name="patientId">The ID of the patient</param>
    /// <returns>List of appointment entities</returns>
    private async Task<List<Appointment>> RetrieveAppointments(System.Guid patientId)
    {
        return await _getAppointmentsByPatientId.Execute(patientId);
    }

    /// <summary>
    /// Map list of Appointment entities to a list of Response models
    /// </summary>
    /// <param name="appointments">List of domain appointments</param>
    /// <returns>Mapped list of response objects</returns>
    private List<GetPatientAppointmentsResponse> MapToResponse(List<Appointment> appointments)
    {
        if (appointments == null || !appointments.Any())
        {
            return new List<GetPatientAppointmentsResponse>();
        }
        return appointments.Select(a => new GetPatientAppointmentsResponse
        {
            Id = a.Id,
            AppointmentTime = a.AppointmentTime,
            Status = a.Status.ToString(),
            Notes = a.Notes,
            DoctorName = a.Doctor != null ? a.Doctor.FullName : string.Empty,
            FacilityName = a.Facility != null ? a.Facility.NameVi : string.Empty,
            SpecialtyName = a.Specialty != null ? a.Specialty.NameVi : string.Empty
        }).ToList();
    }

    /// <summary>
    /// Create API response
    /// </summary>
    /// <param name="responseData">The mapped list of response data</param>
    /// <returns>A formatted ApiResponse</returns>
    private ApiResponse<List<GetPatientAppointmentsResponse>> CreateResponse(List<GetPatientAppointmentsResponse> responseData)
    {
        return ApiResponse<List<GetPatientAppointmentsResponse>>.Success(
            MessageCode.APP_MESSAGE_2000.ToString(),
            responseData
        );
    }
}