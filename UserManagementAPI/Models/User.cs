using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    required public string LastName { get; set; }
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string? FirstName { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }
    [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters.")]
    public string? Role { get; set; }
}
