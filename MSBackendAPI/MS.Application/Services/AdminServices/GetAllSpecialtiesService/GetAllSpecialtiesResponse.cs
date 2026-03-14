namespace MS.Application.Services.AdminServices.GetAllSpecialtiesService
{
    /// <summary>
    /// Response model for specialty list.
    /// </summary>
    public class GetAllSpecialtiesResponse
    {
        /// Vietnamese name
        public string NameVi { get; set; }

        /// English name
        public string NameEn { get; set; }

        /// Vietnamese description
        public string DescriptionVi { get; set; }

        /// English description
        public string DescriptionEn { get; set; }

        /// Icon url for UI
        public string IconUrl { get; set; }

        /// Soft delete flag
        public bool IsDeleted { get; set; }
    }
}