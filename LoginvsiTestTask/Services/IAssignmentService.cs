using LoginvsiTestTask.Models;
using Task = System.Threading.Tasks.Task;

namespace LoginvsiTestTask.Services;

public interface IAssignmentService
{
    /// <summary>
    /// Asynchronously creates a new user.
    /// </summary>
    /// <param name="assignment">The assignment object to create.</param>
    /// <returns>A task representing the asynchronous create operation. The task result contains the created user.</returns>
    Task<Assignment> CreateAssignmentAsync(Assignment assignment);

    /// <summary>
    /// Asynchronously finds an assignment by its ID.
    /// </summary>
    /// <param name="assignmentId">The ID of the assignment to find.</param>
    /// <returns>A task representing the asynchronous find operation.
    /// The task result contains the found assignment.</returns>
    /// <exception cref="KeyNotFoundException"> Thrown if assignment with given id is not found</exception>
    Task<Assignment> FindAssignmentAsync(Guid assignmentId);

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains a read-only collection of users.</returns>
    /// <exception cref="KeyNotFoundException"> Thrown if user with given id is not found</exception>
    Task<IReadOnlyCollection<Assignment>> GetAllAssignmentsAsync();

    Task ReassignUsers(DateTime fromDate);
}