using LoginvsiTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginvsiTestTask.Services;

/// <summary>
/// <see cref="IAssignmentService"/>
/// </summary>
public class AssignmentService(AssignmentDbContext context, ILogger<UserService> logger, IAssignUserService assignUserService) 
    : IAssignmentService
{
    private const int MaxTransitionCount = 3;
    public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
    {
        var user = assignUserService.FindRandomUser(context.Users);
        assignment.AssignedUser = user;
        context.Assignments.Add(assignment);
        await context.SaveChangesAsync();
        logger.LogDebug($"User with id {assignment.Id} is created");
        return assignment;
    }

    public async Task<Assignment> FindAssignmentAsync(Guid assignmentId)
    {
        try
        {
            var user = await context.Assignments.FirstAsync(a => a.Id == assignmentId);
            return user;
        }
        catch (InvalidOperationException e)
        {
            logger.LogError($"Assignment with id {assignmentId} was not found");
            throw new KeyNotFoundException("Assignment not found", e);
        }
    }

    public async Task<IReadOnlyCollection<Assignment>> GetAllAssignmentsAsync()
    {
        var result = await context.Assignments.ToListAsync();
        return result;
    }

    public async Task ReassignUsers(DateTime fromDate)
    {
        var assignmentsToClose = await context.Assignments.Where(a => a.LastUpdateDate < fromDate
                                                               && a.State != AssignmentState.Completed
                                                               && a.TransitionCount>=MaxTransitionCount).ToListAsync();
        foreach (var assigment in assignmentsToClose)
        {
            assigment.AssignedUser = null;
            assigment.State = AssignmentState.Completed;
        }
        await context.SaveChangesAsync();
//        var users = context.
    }
}