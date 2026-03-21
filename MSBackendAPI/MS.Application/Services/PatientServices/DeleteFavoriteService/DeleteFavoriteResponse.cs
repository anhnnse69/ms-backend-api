namespace MS.Application.Services.PatientServices.DeleteFavoriteService
{
    /// <summary>
    /// Represents the response returned after successfully removing a favorite.
    /// </summary>
    public class DeleteFavoriteResponse
    {
        /// <summary>Indicates whether the favorite was successfully removed.</summary>
        public bool IsDeleted { get; set; }
    }
}
