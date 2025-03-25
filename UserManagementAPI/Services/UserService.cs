using UserManagementAPI.Models;

namespace UserManagementAPI;

public class UserService
{
    private static List<User> users = new List<User>
    {
        new User { Id = 1, LastName = "Hawkins", FirstName= "Jim", Email = "jim.Hawkings@example.com", Role = "Cabin boy"},
        new User { Id = 2, LastName = "Silver", FirstName= "Long John", Email = "barbeque@example.com", Role = "Ship cook and mutiny leader" },
        new User { Id = 3, LastName = "David", FirstName= "Livesey", Email = "doctor.livesey@example.com",  Role = "Ship's doctor" },
        new User { Id = 4, LastName = "Trelawney", FirstName= "John", Email = "squire.trewlawney@example.com", Role = "Ship owner" },
        new User { Id = 5, LastName = "Smollett", FirstName= "Alexander", Email = "captain.smollet@example.com", Role = "Ship captain" }
    };

    public IEnumerable<User> GetUsers()
    {
        return users;
    }

    public User? GetUser(int id)
    {
        return users.FirstOrDefault(u => u.Id == id) ?? null;
    }

    public User CreateUser(User user)
    {
        user.Id = users.Max(u => u.Id) + 1;
        users.Add(user);
        return user;
    }

    public User? UpdateUser(int id, User user)
    {
        var existingUser = users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return null;
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;

        return existingUser;
    }

    public void DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            users.Remove(user);
        }
    }
}
