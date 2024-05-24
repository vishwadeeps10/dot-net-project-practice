using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student name is required.")]
        //[StringLength(10, ErrorMessage = "Name should be 10 char long.")]
        [RegularExpression("^[^0-9]*$", ErrorMessage = "The name must not contain any numbers.")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address char is too long.")]
        public string Address { get; set; }

        // [DateCheck]
        // public DateTime AddimisonDate { get; set; }

    }
}
