using System.ComponentModel.DataAnnotations;

namespace ESTORE.Models.Signup
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "User name is required")]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
        public required string ConfirmPassword { get; set; } 
    }
}
