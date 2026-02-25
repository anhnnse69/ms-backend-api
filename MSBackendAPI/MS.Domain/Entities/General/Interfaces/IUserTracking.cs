namespace MS.Domain.Entities.General.Interfaces;

public interface IUserTracking
{
    string CreateBy { get; set; }
    string LastModifiedBy { get; set; }
}
