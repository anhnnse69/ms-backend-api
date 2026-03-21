namespace MS.Application.Services.PatientServices.GetFavoritesService
{
    /// <summary>
    /// Represents a single item in the patient's favorites list.
    /// </summary>
    public class GetFavoritesResponse
    {
        /// <summary>The unique identifier of the favorite record.</summary>
        public Guid Id { get; set; }
        /// <summary>The type of favorite: "Doctor" or "Facility".</summary>
        public string FavoriteType { get; set; } = string.Empty;
        /// <summary>The unique identifier of the favorited doctor, if applicable.</summary>
        public Guid? DoctorId { get; set; }
        /// <summary>The unique identifier of the favorited facility, if applicable.</summary>
        public Guid? FacilityId { get; set; }
        /// <summary>The display name of the doctor or facility.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>The avatar or logo URL of the doctor or facility.</summary>
        public string? Avatar { get; set; }
        /// <summary>The specialty name of the doctor, if applicable.</summary>
        public string? Specialty { get; set; }
        /// <summary>The address of the facility, if applicable.</summary>
        public string? Address { get; set; }
        /// <summary>The timestamp when the favorite was saved.</summary>
        public DateTimeOffset SavedAt { get; set; }
    }
}
