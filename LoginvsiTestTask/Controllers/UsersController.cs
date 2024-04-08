using AutoMapper;
using Flurl;
using LoginvsiTestTask.DTOs;
using LoginvsiTestTask.Models;
using LoginvsiTestTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginvsiTestTask.Controllers;

/// <summary>
/// Controller for performing operations on users.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController(IMapper mapper, IUserService userService) : ControllerBase
{
    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains an array of UserDTO objects.</returns>
    /// <response code="200">The users were successfully retrieved.</response>

    [HttpGet("")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDTO[]>> GetUsers()
    {
        var users = await userService.GetAllUsersAsync();
        var dto = mapper.Map<User[], UserDTO[]>(users.ToArray());
        return Ok(dto);
    }

    /// <summary>
    /// Retrieves a user by the specified user ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a UserDTO object representing the retrieved user.</returns>
    /// <response code="200">The user was successfully retrieved.</response>
    /// <response code="404">The user with the specified ID was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO>> GetUser(Guid id)
    {
        try
        {
            var user = await userService.FindUserAsync(id);
            var dto = mapper.Map<UserDTO>(user);
            return Ok(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="newUser">The CreateUserDTO object containing the information of the user to be created.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the created UserDTO object.</returns>
    /// <response code="200">The user was successfully created.</response>
    /// <response code="400">If input data is wrong.</response>
    /// <response code="409">If user with given name already exists.</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody]CreateUserDTO newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var user = mapper.Map<User>(newUser);
            user = await userService.CreateUserAsync(user);
            var dto = mapper.Map<UserDTO>(user);
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            var objectUrl = currentUrl.AppendPathSegment(dto.Id.ToString()).ToString();
            return Created(objectUrl, dto);
        }
        catch (ArgumentException)
        {
            return Conflict(); 
        }
    }
}