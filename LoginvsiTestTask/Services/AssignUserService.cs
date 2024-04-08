using LoginvsiTestTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace LoginvsiTestTask.Services;

/// <summary>
/// <see cref="IAssignUserService"/>
/// </summary>
public class AssignUserService(AssignmentDbContext context, ILogger<UserService> logger, IRandomService random)
    : IAssignUserService
{
    private const int MaxTransitionCount = 3;

    private async Task CompleteAssignments(DateTime fromDate)
    {
        var assignments = await context.Assignments
            .Where(a => a.LastUpdateDate >= fromDate
                   && a.State != AssignmentState.Completed
                   && a.TransitionCount >= MaxTransitionCount)
            .ToListAsync();
        foreach (var assigment in assignments)
        {
            assigment.AssignedUser = null;
            assigment.AssignedUserId = null;
            assigment.State = AssignmentState.Completed;
            logger.LogInformation($"Assignment with ID {assigment.Id} is moved to Completed state. ");
        }
        await context.SaveChangesAsync();
    }

    private IQueryable<User> AvailableUsers(IQueryable<User> users, User? userToExclude = null)
    {
        var availableUsers = users;
        if (userToExclude != null)
        {
            availableUsers = availableUsers.Where(u => u.Id != userToExclude.Id);
        }
        return availableUsers;
    }

    // This method is sync as it is called for DbSet AND List<>; List does not support async operation, so 
    //selected maintainability having lack in performance
    public  User? FindRandomUser(IQueryable<User> users, User? userToExclude = null)
    {
        var count = AvailableUsers(users, userToExclude).Count();
        var index = random.Next(count);
        var user = AvailableUsers(users, userToExclude).Skip(index).FirstOrDefault();
        return user;
    }

    public async Task ReassignUsers(DateTime fromDate)
    {
        await CompleteAssignments(fromDate);
        var assignments = await context.Assignments
            .Where(a => a.LastUpdateDate >= fromDate
                        && a.State != AssignmentState.Completed
                        && a.TransitionCount < MaxTransitionCount)
            .Include(a => a.AssignedUser).ToListAsync();
        var users = await AvailableUsers(context.Users).Include(u=>u.Assignments).ToListAsync();
        foreach (var assigment in assignments)
        {
            var user = FindRandomUser(users.AsQueryable(), assigment.AssignedUser);
            logger.LogInformation($"Assignment ID: {assigment.Id}, " +
                                  $"Assigned User: '{assigment.AssignedUser?.Id}' => '{user?.Id}', " +
                                  $"Transition Count: '{assigment.TransitionCount+1}'");
            if (assigment.AssignedUser!=null && user!=null && assigment.AssignedUser.Id == user.Id)
            {
                throw new ApplicationException("Transferring to the same user - logic error");
            }
            assigment.AssignedUser = user;
            assigment.AssignedUserId = user?.Id;
            assigment.TransitionCount += 1;
        }
        await context.SaveChangesAsync();
    }
}