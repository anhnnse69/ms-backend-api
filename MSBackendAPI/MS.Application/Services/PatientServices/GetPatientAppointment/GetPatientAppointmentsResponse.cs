namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
    /// <summary>
    /// Response model containing a list of appointments
    /// </summary>
    public class GetPatientAppointmentsResponse
    {
        public List<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
    }
}
