using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

/// <summary>
/// Controller for managing users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _userService.GetUsers();
        if (users == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "No users found.",
                Details = "There are no users in the system."
            });
        }
        return Ok(users);
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user with the specified ID.</returns>
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid user ID.",
                Details = "The user ID must be greater than 0."
            });
        }

        var user = _userService.GetUser(id);
        if (user == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = $"User with ID {id} not found.",
                Details = "The user ID provided does not exist in the system."
            });
        }
        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>The created user.</returns>
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdUser =_userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="updatedUser">The updated user data.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, User updatedUser)
    {
         if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _userService.GetUser(id);
        if (user == null)
        {
             return NotFound(new ErrorResponse
            {
                Message = $"User with ID {id} not found.",
                Details = "The user ID provided does not exist in the system."
            });
        }

        user.LastName = updatedUser.LastName;
        user.FirstName = updatedUser.FirstName;
        user.Role = updatedUser.Role;
        user.Email = updatedUser.Email;

        return NoContent();
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _userService.GetUser(id);
        if (user == null)
        {
             return NotFound(new ErrorResponse
            {
                Message = $"User with ID {id} not found.",
                Details = "The user ID provided does not exist in the system."
            });
        }

        _userService.DeleteUser(user.Id);
        return NoContent();
    }
}
