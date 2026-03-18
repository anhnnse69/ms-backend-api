namespace MS.Application.Services.PatientServices.SearchDoctorService
{
    /// <summary>
    /// Represents a facility information item in a doctor search result.
    /// </summary>
    public class SearchDoctorFacilityResponse
    {
        /// <summary>The unique identifier of the facility.</summary>
        public Guid Id { get; set; }
        /// <summary>The name of the facility.</summary>
        public string Name { get; set; }
        /// <summary>The address of the facility.</summary>
        public string Address { get; set; }
        /// <summary>The city where the facility is located.</summary>
        public string City { get; set; }
    }
}
