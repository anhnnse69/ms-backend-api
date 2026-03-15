using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientAppointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
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
        /// <returns>API Response with appointment data</returns>
        public async Task<ApiResponse<GetPatientAppointmentsResponse>> Process(GetPatientAppointmentsRequest request)
        {
            // 1. Retrieve appointments data from database
            var appointments = await RetrieveAppointments(request.PatientId);
            // 2. Map domain entities to DTOs
            var responseData = MapToResponse(appointments);
            // 3. Create response
            return CreateResponse(responseData);
        }

        /// <summary>
        /// Retrieve appointments by patient ID
        /// </summary>
        /// <param name="patientId">The ID of the patient</param>
        /// <returns>List of appointment entities</returns>
        private async Task<List<Appointment>> RetrieveAppointments(Guid patientId)
        {
            return await _getAppointmentsByPatientId.Execute(patientId);
        }

        /// <summary>
        /// Map list of Appointment entities to Response model
        /// </summary>
        /// <param name="appointments">List of domain appointments</param>
        /// <returns>Mapped response object</returns>
        private GetPatientAppointmentsResponse MapToResponse(List<Appointment> appointments)
        {
            var response = new GetPatientAppointmentsResponse();
            if (appointments == null || !appointments.Any())
            {
                return response;
            }
            response.Appointments = appointments.Select(a => new AppointmentDto
            {
                // Map Id
                Id = a.Id,
                // Map Appointment Time
                AppointmentTime = a.AppointmentTime,
                // Map Status (Convert enum to string for UI)
                Status = a.Status.ToString(),
                // Map Notes
                Notes = a.Notes,
                // Map Doctor Name (Fallback to null/empty if Doctor is null)
                DoctorName = a.Doctor != null ? a.Doctor.FullName : string.Empty,
                // Map Facility Name (Use NameVi or NameEn depending on your localization logic)
                FacilityName = a.Facility != null ? a.Facility.NameVi : string.Empty,
                // Map Specialty Name
                SpecialtyName = a.Specialty != null ? a.Specialty.NameVi : string.Empty
            }).ToList();
            return response;
        }

        /// <summary>
        /// Create API response
        /// </summary>
        /// <param name="responseData">The mapped response data</param>
        /// <returns>A formatted ApiResponse</returns>
        private ApiResponse<GetPatientAppointmentsResponse> CreateResponse(GetPatientAppointmentsResponse responseData)
        {
            // Create and return the success response with general success message code 2000
            return ApiResponse<GetPatientAppointmentsResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                responseData
            );
        }
    }
}
