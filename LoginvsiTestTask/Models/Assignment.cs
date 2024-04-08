using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginvsiTestTask.Models;

[Table("Assignment")]
public class Assignment : IVersionedEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(1024)] 
    public string Description { get; set; } = String.Empty;

    public Guid? AssignedUserId { get; set; }

    public User? AssignedUser { get; set; } = null!;

    public AssignmentState State { get; set; }
    
    public int TransitionCount { get; set; }

    public DateTime LastUpdateDate { get; set; }

    [ConcurrencyCheck]
    public Guid RowVersion { get; set; }
}