using System.ComponentModel.DataAnnotations;

namespace EcomWeb.Dtos.Role
{
    public class RoleAddDto
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Description cannot be more than 50 characters.")]
        public string? Description { get; set; }
    }
}
