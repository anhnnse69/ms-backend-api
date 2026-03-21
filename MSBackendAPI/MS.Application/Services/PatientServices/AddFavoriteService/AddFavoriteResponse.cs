namespace MS.Application.Services.PatientServices.AddFavoriteService
{
    /// <summary>
    /// Represents the response returned after successfully adding a favorite.
    /// </summary>
    public class AddFavoriteResponse
    {
        /// <summary>The unique identifier of the created or restored favorite record.</summary>
        public Guid Id { get; set; }
        /// <summary>The unique identifier of the patient who owns this favorite.</summary>
        public Guid PatientId { get; set; }
        /// <summary>The unique identifier of the favorited doctor, if applicable.</summary>
        public Guid? DoctorId { get; set; }
        /// <summary>The unique identifier of the favorited facility, if applicable.</summary>
        public Guid? FacilityId { get; set; }
        /// <summary>The timestamp when the favorite was created or restored.</summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}
