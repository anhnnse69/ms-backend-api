namespace MS.Domain.Entities.General.Interfaces;

/// <summary>
/// Interface for entities that support soft delete (logical deletion)
/// Instead of removing records, they are marked as deleted
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}
