using System.ComponentModel.DataAnnotations;

namespace EcomWeb.Dtos.Customer
{
    public class CustomerAddDto
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(150, ErrorMessage = "Full name cannot be longer than 150 characters")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(150, ErrorMessage = "Email cannot be longer than 150 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters long")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Phone must be between 8 and 12 numbers")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "District is required")]
        public string District { get; set; }

        [Required(ErrorMessage = "Ward is required")]
        public string Ward { get; set; }

        public bool Active { get; set; }
    }
}
