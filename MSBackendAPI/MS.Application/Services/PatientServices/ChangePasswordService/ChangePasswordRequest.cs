using System.Text.Json.Serialization;

namespace MS.Application.Services.PatientServices.ChangePasswordService;

/// <summary>
/// Request model for change password
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// User Id
    /// </summary>
    [JsonIgnore]
    public Guid UserId { get; set; }

    /// <summary>
    /// Current password
    /// </summary>
    public string CurrentPassword { get; set; }

    /// <summary>
    /// New password
    /// </summary>
    public string NewPassword { get; set; }
}