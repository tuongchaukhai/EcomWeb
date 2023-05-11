using System.ComponentModel.DataAnnotations;

namespace EcomWeb.Dtos.Customer
{
    public class CustomerUpdateDto
    {
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters long")]
        //public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;

        public int District { get; set; }

        public int Ward { get; set; }

        public bool Active { get; set; }
    }
}
