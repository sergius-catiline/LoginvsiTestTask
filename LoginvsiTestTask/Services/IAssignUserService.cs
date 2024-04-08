using LoginvsiTestTask.Models;

namespace LoginvsiTestTask.Services;

/// <summary>
/// Represents a service for assigning users to tasks.
/// </summary>
public interface IAssignUserService
{
    /// <summary>
    /// Reassigns users to incomplete assignments that were last updated on or after a specified date.
    /// </summary>
    /// <param name="fromDate">The date from which to include assignments for reassignment.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ReassignUsers(DateTime fromDate);

    /// <summary>
    /// Finds a random user from a collection of users.
    /// </summary>
    /// <param name="users">The collection of users to choose from.</param>
    /// <param name="userToExclude">The user to exclude from the random selection. (optional)</param>
    /// <returns>The randomly selected user from the collection.</returns>
    User? FindRandomUser(IQueryable<User> users, User? userToExclude = null);
}