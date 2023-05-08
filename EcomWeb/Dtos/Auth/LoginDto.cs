using System.ComponentModel.DataAnnotations;

namespace EcomWeb.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
