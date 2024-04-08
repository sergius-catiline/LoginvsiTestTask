using AutoMapper;
using Flurl;
using LoginvsiTestTask.DTOs;
using LoginvsiTestTask.Models;
using LoginvsiTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginvsiTestTask.Controllers;

/// <summary>
/// Controller for managing Tasks.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AssignmentsController(
    IMapper mapper,
    IAssignmentService assignmentService) : ControllerBase
{

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains an array of UserDTO objects.</returns>
    /// <response code="200">The users were successfully retrieved.</response>

    [HttpGet("")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDTO[]>> GetAssignment()
    {
        var assignments = await assignmentService.GetAllAssignmentsAsync();
        var dto = mapper.Map<Assignment[], AssignmentDTO[]>(assignments.ToArray());
        return Ok(dto);
    }
    
    /// <summary>
    /// Retrieves an assignment by its ID.
    /// </summary>
    /// <param name="id">The ID of the assignment.</param>
    /// <returns>
    /// An ActionResult representing the assignment with the specified ID.
    /// If the assignment is found, it returns the AssignmentDTO object.
    /// If the assignment is not found, it returns a NotFound result.
    /// </returns>
    /// <response code="200">The assignment was successfully retrieved.</response>
    /// <response code="404">The assignment with the specified ID was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssignmentDTO>> GetAssignment(Guid id)
    {
        try
        {
            var assignment = await assignmentService.FindAssignmentAsync(id);
            var dto = mapper.Map<AssignmentDTO>(assignment);
            return Ok(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Creates a new assignment asynchronously.
    /// </summary>
    /// <param name="newAssignment">The CreateAssignmentDTO object containing the information of the assignment to be created.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the created assignmentDTO object.</returns>
    /// <response code="200">The assignment was successfully created.</response>
    /// <response code="400">If input data is wrong.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AssignmentDTO>> CreateAssignment([FromBody] CreateAssignmentDTO newAssignment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var assignment = mapper.Map<Assignment>(newAssignment);
        assignment = await assignmentService.CreateAssignmentAsync(assignment);
        var dto = mapper.Map<AssignmentDTO>(assignment);
        var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
        var objectUrl = currentUrl.AppendPathSegment(dto.Id.ToString()).ToString();
        return Created(objectUrl, dto);
    }
}
