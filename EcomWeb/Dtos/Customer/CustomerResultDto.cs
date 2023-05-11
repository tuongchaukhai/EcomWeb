using EcomWeb.Models;

namespace EcomWeb.Dtos.Customer
{
    public class CustomerResultDto
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        public string? Avatar { get; set; }

        public string Email { get; set; } = null!;

        //public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int District { get; set; }

        public int Ward { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool Active { get; set; }

    }
}
