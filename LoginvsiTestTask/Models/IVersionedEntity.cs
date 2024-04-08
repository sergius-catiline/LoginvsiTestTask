namespace LoginvsiTestTask.Models;

/// <summary>
/// Needed for optimistic concurrency implementation. 
/// As SQLLite does not has an analog of MS SQL rowversion column,
/// so no easy way to use [Timestamp] attribute on byte[] field in EF.
/// This interface helps to implement it manually to handle optimistic concurrency on the app level 
/// </summary>
public interface IVersionedEntity
{
    public Guid RowVersion { get; set; }
}