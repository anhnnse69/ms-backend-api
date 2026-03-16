namespace MS.Application.Services.AdminServices.DeleteSpecialtyService
{
    /// <summary>
    /// Response model representing the result of specialty deletion
    /// </summary>
    public class DeleteSpecialtyResponse
    {
        /// <summary>
        /// Indicates whether the specialty was successfully deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes response model
        /// </summary>
        /// <param name="isDeleted"></param>
        public DeleteSpecialtyResponse(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}