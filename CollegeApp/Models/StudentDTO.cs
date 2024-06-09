using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enrollment Number is required.")]
        [StringLength(100)]
        public string Entollment_no { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name should be 100 char long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Fathers name should be 100 char long.")]
        public string Fathers_name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        [StringLength(60)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_of_birth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(400)]
        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Added_On { get; set; } = DateTime.Now;

    }
}
