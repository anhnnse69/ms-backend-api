namespace MS.Domain.Enums.Types
{
    /// <summary>
    /// Represents the type of notification event (the business event that triggered the notification).
    /// </summary>
    public enum NotificationType
    {
        AppointmentReminder,
        AppointmentConfirmed,
        AppointmentCancelled,
        AppointmentStatusChanged,
    }
}