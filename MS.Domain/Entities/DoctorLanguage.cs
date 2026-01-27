using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Languages that a doctor can communicate in
    /// </summary>
    public class DoctorLanguage
    {
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public Language Language { get; set; }
    }
}
