
namespace MS.Application.Services.ManagerServices.FacilitySpecialtyService
{
    /// <summary>
    /// Response model for specialty details
    /// </summary>
    public class FacilitySpecialtyResponse
    {
        public Guid SpecialtyId { get; set; }
        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; }
        public string DescriptionEn { get; set; }
        public string IconUrl { get; set; }
    }
}
