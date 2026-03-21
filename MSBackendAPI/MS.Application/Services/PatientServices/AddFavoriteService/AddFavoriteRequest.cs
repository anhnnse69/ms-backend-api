namespace MS.Application.Services.PatientServices.AddFavoriteService
{
    /// <summary>
    /// Represents the request payload for adding a doctor or facility to the patient's favorites.
    /// </summary>
    public class AddFavoriteRequest
    {
        /// <summary>The unique identifier of the doctor to favorite (null if favoriting a facility).</summary>
        public Guid? DoctorId { get; set; }
        /// <summary>The unique identifier of the facility to favorite (null if favoriting a doctor).</summary>
        public Guid? FacilityId { get; set; }
    }
}
