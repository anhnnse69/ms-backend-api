using MS.Domain.Enums.Types;

namespace MS.Application.Services.AdminServices.FaclityService.CreateFacilityService
{
    /// <summary>
    /// Request object for creating a facility.
    /// </summary>
    public class CreateFacilityRequest
    {
        /// Vietnamese name of the facility
        public string NameVi { get; set; }

        /// English name of the facility
        public string NameEn { get; set; }

        /// Vietnamese description
        public string DescriptionVi { get; set; }

        /// English description
        public string DescriptionEn { get; set; }

        /// Logo image URL
        public string LogoUrl { get; set; }

        /// Address of the facility
        public string Address { get; set; }

        /// Contact phone number
        public string Phone { get; set; }

        /// Contact email
        public string Email { get; set; }

        /// City where the facility is located
        public string City { get; set; }

        /// Type of facility
        public FacilityType Type { get; set; }
    }
}