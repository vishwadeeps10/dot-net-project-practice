using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Cast the value to a nullable DateTime
            var date = value as DateTime?;

            // Check if the date is less than today
            if (date.HasValue && date.Value <= DateTime.Now)
            {
                return new ValidationResult("The date must be greater than or equal to today's date.");
            }

            // If the date is valid, return success
            return ValidationResult.Success;
        }
    }
}
