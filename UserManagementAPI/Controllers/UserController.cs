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
    private static List<User> users = new List<User>
    {
        new User { Id = 1, LastName = "Hawkins", FirstName= "Jim", Email = "jim.Hawkings@example.com", Role = "Cabin boy"},
        new User { Id = 2, LastName = "Silver", FirstName= "Long John", Email = "barbeque@example.com", Role = "Ship cook and mutiny leader" },
        new User { Id = 3, LastName = "David", FirstName= "Livesey", Email = "doctor.livesey@example.com",  Role = "Ship's doctor" },
        new User { Id = 4, LastName = "Trelawney", FirstName= "John", Email = "squire.trewlawney@example.com", Role = "Ship owner" },
        new User { Id = 5, LastName = "Smollett", FirstName= "Alexander", Email = "captain.smollet@example.com", Role = "Ship captain" }
    };

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
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

        var user = users.FirstOrDefault(u => u.Id == id);
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
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
        var user = users.FirstOrDefault(u => u.Id == id);
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
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
             return NotFound(new ErrorResponse
            {
                Message = $"User with ID {id} not found.",
                Details = "The user ID provided does not exist in the system."
            });
        }

        users.Remove(user);
        return NoContent();
    }
}
