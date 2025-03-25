using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>
    {
        new User { Id = 1, LastName = "John Doe", Email = "john.doe@example.com" },
        new User { Id = 2, LastName = "Jane Smith", Email = "jane.smith@example.com" }
    };

    // GET: api/User
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        user.LastName = updatedUser.LastName;
        user.FirstName = updatedUser.FirstName;
        user.Role = updatedUser.Role;   
        user.Email = updatedUser.Email;

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        users.Remove(user);
        return NoContent();
    }
}
