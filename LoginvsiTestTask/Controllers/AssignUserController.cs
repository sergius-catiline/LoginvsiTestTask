using LoginvsiTestTask.Models;
using LoginvsiTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginvsiTestTask.Controllers;

/// <summary>
/// User fore TEST purpose only
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AssignUserController(IAssignUserService service) 
    : ControllerBase
{
    /// <summary>
    /// Reassign tasks on users 
    /// </summary>
    [HttpPost]
    public async Task Create()
    {
        await service.ReassignUsers(DateTime.MinValue);
    }

}