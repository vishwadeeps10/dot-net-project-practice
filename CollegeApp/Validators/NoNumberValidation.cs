using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CollegeApp.Validators
{
    public class NoNumberValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Ensure the value is a string
            var name = value as string;

            // If the value is null or empty, we consider it valid (other validators can handle null/empty checks)
            if (string.IsNullOrEmpty(name))
            {
                return ValidationResult.Success;
            }

            // Check if the string contains any digits
            if (Regex.IsMatch(name, @"\d"))
            {
                return new ValidationResult("The name must not contain any numbers.");
            }

            return ValidationResult.Success;
        }
    }
}

