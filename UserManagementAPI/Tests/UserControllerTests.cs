using Microsoft.AspNetCore.Mvc;
using UserManagementAPI;
using UserManagementAPI.Controllers;
using UserManagementAPI.Models;
using Xunit;

public class UserControllerTests
{
    private readonly UserService _userService;

    public UserControllerTests()
    {
        _userService = new UserService();
    }

    
    [Fact]
    public void GetUsers_ReturnsAllUsers()
    {
        // Arrange
        var controller = new UserController(_userService);

        // Act
        var result = controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var users = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(5, users.Count); // Assuming there are 5 users in the static list
    }

    [Fact]
    public void GetUser_ReturnsUser_WhenIdIsValid()
    {
        // Arrange
        var controller = new UserController(_userService);
        int validId = 1;

        // Act
        var result = controller.GetUser(validId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var user = Assert.IsType<User>(okResult.Value);
        Assert.Equal(validId, user.Id);
    }

    [Fact]
    public void GetUser_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        var controller = new UserController(_userService);
        int invalidId = 999;

        // Act
        var result = controller.GetUser(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void CreateUser_AddsUserAndReturnsCreatedAtAction()
    {
        // Arrange
        var controller = new UserController(_userService);
        var newUser = new User { FirstName = "Test", LastName = "User", Email = "test.user@example.com", Role = "Tester" };

        // Act
        var result = controller.CreateUser(newUser);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdUser = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(6, createdUser.Id); // Assuming the new user gets ID 6
    }

    [Fact]
    public void DeleteUser_RemovesUser_WhenIdIsValid()
    {
        // Arrange
        var controller = new UserController(_userService);
        int validId = 1;

        // Act
        var result = controller.DeleteUser(validId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.DoesNotContain(controller.GetUsers().Value, u => u.Id == validId);
    }
}