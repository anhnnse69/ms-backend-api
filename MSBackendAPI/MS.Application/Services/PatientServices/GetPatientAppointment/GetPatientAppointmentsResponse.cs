
namespace MS.Application.Services.PatientServices.GetPatientAppointment
{
    /// <summary>
    /// Response model representing a single appointment data.
    /// The API will return a List of this model.
    /// </summary>
    public class GetPatientAppointmentsResponse
    {
        public Guid Id { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string DoctorName { get; set; }
        public string FacilityName { get; set; }
        public string SpecialtyName { get; set; }
    }
}