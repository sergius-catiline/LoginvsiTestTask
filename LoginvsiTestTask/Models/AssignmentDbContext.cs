using Microsoft.EntityFrameworkCore;

namespace LoginvsiTestTask.Models;

/// <summary>
/// Database context for the entities 
/// </summary>
public class AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Assignment> Assignments { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntities = ChangeTracker.Entries() //implementing manually optimistic concurrency 
            .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);

        foreach(var entry in modifiedEntities)
        {
            if (entry.Entity is IVersionedEntity entity)
            {
                entity.RowVersion = Guid.NewGuid();
            }
            if (entry.Entity is Assignment assignment)
            {
                assignment.LastUpdateDate = DateTime.Now;
            }

        }
        return base.SaveChangesAsync(cancellationToken);
    }
}