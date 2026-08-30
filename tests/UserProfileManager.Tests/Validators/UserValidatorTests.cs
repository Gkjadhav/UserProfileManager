using UserProfileManager.Models;
using UserProfileManager.Validators;

namespace UserProfileManager.Tests.Validators;

public class UserValidatorTests
{
    [Fact]
    public void Validate_WithValidUser_ReturnsNoErrors()
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Software Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        // Act
        var errors = UserValidator.Validate(user);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void Validate_WithMissingFullName_ReturnsError()
    {
        // Arrange
        var user = new User
        {
            FullName = string.Empty,
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Software Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        // Act
        var errors = UserValidator.Validate(user);

        // Assert
        Assert.NotEmpty(errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("notanemail")]
    [InlineData("john@")]
    public void Validate_WithInvalidEmail_ReturnsError(string invalidEmail)
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = invalidEmail,
            UserInfo = "Software Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        // Act
        var errors = UserValidator.Validate(user);

        // Assert
        Assert.NotEmpty(errors);
    }

    [Theory]
    [InlineData("not a url")]
    [InlineData("http://www.linkedin.com/in/johndoe")]
    [InlineData("https://www.notlinkedin.com/in/johndoe")]
    public void Validate_WithInvalidLinkedInUrl_ReturnsError(string invalidLinkedInUrl)
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Software Developer",
            LinkedInProfile = invalidLinkedInUrl
        };

        // Act
        var errors = UserValidator.Validate(user);

        // Assert
        Assert.NotEmpty(errors);
    }
}