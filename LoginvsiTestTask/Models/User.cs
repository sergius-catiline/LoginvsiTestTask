using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginvsiTestTask.Models;

/// <summary>
/// User Entity
/// </summary>
[Table("User")]
[Index(nameof(Name), IsUnique = true)]
public class User : IVersionedEntity
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(256)] 
    public string Name { get; set; } = String.Empty;

    public ICollection<Assignment> Assignments { get; set; } = null!;

    [ConcurrencyCheck]
    public Guid RowVersion { get; set; }
}