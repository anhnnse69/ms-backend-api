using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class MessageTranslation : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
