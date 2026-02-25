namespace MS.Domain.Entities.General.Interfaces;

public interface IDateTracking
{
    DateTimeOffset CreateDate { get; set; }
    DateTimeOffset? LastModifiedDate { get; set; }
}
