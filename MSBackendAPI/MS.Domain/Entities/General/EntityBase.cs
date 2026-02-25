using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities.General;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
