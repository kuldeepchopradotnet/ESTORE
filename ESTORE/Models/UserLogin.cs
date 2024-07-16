using System.ComponentModel.DataAnnotations;

namespace ESTORE.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email is requred")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

    }
}
