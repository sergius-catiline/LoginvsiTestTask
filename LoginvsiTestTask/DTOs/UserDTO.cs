namespace LoginvsiTestTask.DTOs;

/// <summary>
/// DTO for a user.
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Represents the unique identifier of a user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Represents the name of a user.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Represents a collection of assignment DTOs.
    /// </summary>
    public List<AssignmentDTO> Assignments = new ();
}