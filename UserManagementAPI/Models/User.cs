namespace UserManagementAPI;

public class User
{
    public int Id { get; set; }
    required public string LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}
