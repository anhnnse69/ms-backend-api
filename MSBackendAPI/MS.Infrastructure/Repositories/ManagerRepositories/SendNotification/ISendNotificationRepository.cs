using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.SendNotification
{
    /// <summary>
    /// Contract for saving notifications to patients about appointments.
    /// </summary>
    public interface ISendNotificationRepository
    {
        /// <summary>
        /// Save a notification to the database
        /// </summary>
        Task<Notification> Execute(Notification notification);
    }
}