namespace MS.Application.Services.AdminServices.GetAllFacilitiesService
{
    /// <summary>
    /// Response model for facility list
    /// </summary>
    public class GetAllFacilitiesResponse
    {
        /// Vietnamese name of the facility.
        public string NameVi { get; set; }
        /// English name of the facility.
        public string NameEn { get; set; }
        /// Physical address of the facility.
        public string Address { get; set; }
        /// Contact phone number of the facility.
        public string Phone { get; set; }
        /// Contact email address of the facility.
        public string Email { get; set; }
        /// City where the facility is located.
        public string City { get; set; }
        /// Type of facility.
        public string Type { get; set; }
        /// Indicates whether the facility is active.
        public bool IsActive { get; set; }
        /// URL of the facility logo.
        public string LogoUrl { get; set; }
    }
}