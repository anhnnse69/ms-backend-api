namespace MS.Application.Services.AdminServices.DeleteFacilityService
{
    /// <summary>
    /// Response model representing the result of facility deletion
    /// </summary>
    public class DeleteFacilityResponse
    {
        /// <summary>
        /// Indicates whether the facility was successfully deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes response model
        /// </summary>
        /// <param name="isDeleted"></param>
        public DeleteFacilityResponse(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}