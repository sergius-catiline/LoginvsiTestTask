using System.ComponentModel.DataAnnotations;

namespace LoginvsiTestTask.DTOs;

/// <summary>
/// Represents the data transfer object for creating a new assignment.
/// </summary>
public class CreateAssignmentDTO
{
    /// Represents the state of an assignment.
    /// @property {string} State - The state of the assignment. Can be "Waiting" or "InProgress".
    /// /
    [Required]
    [RegularExpression("Waiting|InProgress")]
    public string State { get; set; } = String.Empty;

    /// <summary>
    /// Represents the description of a create assignment data transfer object (DTO).
    /// </summary>
    public string Description { get; set; } = String.Empty;
}