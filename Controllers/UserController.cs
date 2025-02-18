using Microsoft.AspNetCore.Mvc;
using ASPNetExapp.Models;
using ASPNetExapp.Services;

namespace UsersApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<PaginatedResult<User>> GetUsers(
        [FromQuery] string? query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 2)
    {
        return Ok(_userService.GetUsers(query, page, pageSize));
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        return user != null ? Ok(user) : NotFound(new { message = "User not found" });
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User newUser)
    {
        var createdUser = _userService.CreateUser(newUser);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (!_userService.UpdateUser(id, updatedUser))
            return NotFound(new { message = "User not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        if (!_userService.DeleteUser(id))
            return NotFound(new { message = "User not found" });

        return NoContent();
    }
}
