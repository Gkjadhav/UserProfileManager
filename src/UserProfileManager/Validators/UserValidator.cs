using System.ComponentModel.DataAnnotations;
using UserProfileManager.Models;
using UserProfileManager.Utilities;

namespace UserProfileManager.Validators;

public static class UserValidator
{
    public static IReadOnlyList<string> Validate(User user)
    {
        var errors = new List<string>();

        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();

        // Validate DataAnnotations such as [Required] and [EmailAddress]
        Validator.TryValidateObject(
            user,
            validationContext,
            validationResults,
            validateAllProperties: true);

        // Add DataAnnotations errors
        errors.AddRange(
            validationResults
                .Where(result => !string.IsNullOrWhiteSpace(result.ErrorMessage))
                .Select(result => result.ErrorMessage!));

        // Validate LinkedIn URL separately
        if (!UrlValidator.IsValidLinkedInUrl(user.LinkedInProfile))
        {
            errors.Add(
                "LinkedIn profile URL must be a valid HTTPS linkedin.com URL.");
        }

        return errors;
    }
}