using EcomWeb.Models;

namespace EcomWeb.Dtos.User
{
    public class UserResultDto
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Active { get; set; }

        public string FullName { get; set; } = null!;

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedDate { get; set; }

        public string RoleName { get; set; }

    }
}
