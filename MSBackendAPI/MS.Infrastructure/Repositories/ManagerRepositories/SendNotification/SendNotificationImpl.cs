using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.SendNotification
{
    /// <summary>
    /// Repository implementation for saving notifications to patients
    /// </summary>
    public class SendNotificationImpl : RepositoryBase<Notification, Guid, AppDbContext>, ISendNotificationRepository
    {
        /// <summary>
        /// Initializes a new instance of the SendNotificationImpl class.
        /// </summary>
        /// <param name="context">The database context used to access notification data.</param>
        /// <param name="unitOfWork">The unit of work responsible for managing database transactions.</param>
        public SendNotificationImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Save a notification to the database
        /// </summary>
        /// <param name="notification">The notification entity to save</param>
        /// <returns>The saved notification entity</returns>
        public async Task<Notification> Execute(Notification notification)
        {
            await CreateAsync(notification);
            await SaveChangesAsync();
            return notification;
        }
    }
}