using System.ComponentModel.DataAnnotations;

namespace HealthcareAPI.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email Address is Required.")]
        [StringLength(50)]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
