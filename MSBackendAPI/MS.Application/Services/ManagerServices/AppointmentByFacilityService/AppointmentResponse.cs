namespace MS.Application.Services.ManagerServices.AppointmentByFacilityService
{
    /// <summary>
    /// Response model representing appointment information
    /// </summary>
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
    }
}
