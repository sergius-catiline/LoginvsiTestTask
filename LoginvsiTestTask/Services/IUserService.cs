using LoginvsiTestTask.Models;
using Task = System.Threading.Tasks.Task;

namespace LoginvsiTestTask.Services;

/// <summary>
/// Interface for user manipulations
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Asynchronously creates a new user.
    /// </summary>
    /// <param name="user">The user object containing the user details.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the created user.</returns>
    /// <exception cref="ArgumentException"> Thrown if user with given name already exists</exception>
    Task<User> CreateUserAsync(User user);

    /// <summary>
    /// Asynchronously finds a user by the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to find.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the found user.</returns>
    /// <exception cref="KeyNotFoundException"> Thrown if user with given id is not found</exception>
    Task<User> FindUserAsync(Guid userId);

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains a read-only collection of User objects.</returns>
    Task<IReadOnlyCollection<User>> GetAllUsersAsync();
}