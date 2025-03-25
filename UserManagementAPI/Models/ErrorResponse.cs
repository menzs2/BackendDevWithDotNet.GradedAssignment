namespace UserManagementAPI.Models;
public class ErrorResponse
{
    required public string Message { get; set; }
    required public string Details { get; set; }
}