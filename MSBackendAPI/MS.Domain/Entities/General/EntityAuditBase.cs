using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities.General;

public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
{
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
}
