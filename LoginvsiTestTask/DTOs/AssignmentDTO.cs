namespace LoginvsiTestTask.DTOs;

/// <summary>
/// Represents an assignment data transfer object.
/// </summary>
public class AssignmentDTO
{
    /// <summary>
    /// Represents the identifier of an assignment.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Represents the description property of an AssignmentDTO object.
    /// </summary>
    public string Description { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the ID of the user assigned to the assignment.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier of the user who has been assigned to the assignment.
    /// The assigned user is responsible for completing the assignment.
    /// If no user has been assigned, the value is null.
    /// </remarks>
    public Guid? AssignedUserId { get; set; }

    /// <summary>
    /// Gets or sets the state of the Assignment.
    /// </summary>
    public string State { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the transition count property of an assignment data transfer object that represents the number
    /// of transitions that have occurred for the assignment.
    /// </summary>
    public int TransitionCount { get; set; }
}