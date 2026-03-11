using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    public class MessageTranslation : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
