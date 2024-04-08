using LoginvsiTestTask.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace LoginvsiTestTask.Services;

/// <summary>
/// <see cref="IUserService"/>
/// </summary>
public class UserService(AssignmentDbContext context, ILogger<UserService> logger, IRandomService random): IUserService
{
    private const int MaxAssignmentsPerUser = 5;
    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            logger.LogDebug($"User with id {user.Id} is created");
            return user;
        }
        catch (DbUpdateException e)
        {
            logger.LogError($"User with name {user.Name} already exists");
            throw new ArgumentException("User already exists", e);
        }
    }

    public async Task<User> FindUserAsync(Guid userId)
    {
        try
        {
            var user = await context.Users.FirstAsync(u => u.Id == userId);
            return user;
        }
        catch (InvalidOperationException e)
        {
            logger.LogError($"User with id {userId} was not found");
            throw new KeyNotFoundException("User not found", e);
        }
    }

    public async Task<IReadOnlyCollection<User>> GetAllUsersAsync()
    {
        var result = await context.Users.ToListAsync();
        return result;
    }
}