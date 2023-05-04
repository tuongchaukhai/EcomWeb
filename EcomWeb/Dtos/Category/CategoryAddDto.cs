using System.ComponentModel.DataAnnotations;

namespace EcomWeb.Dtos.Category
{
    public class CategoryAddDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(255, ErrorMessage = "Category name cannot be longer than 255 characters")]
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Parent Id must be a positive integer")]
        public int? ParentId { get; set; }
        [MaxLength(255, ErrorMessage = "Cover URL cannot be longer than 255 characters")]
        public string? Cover { get; set; }
        [Required(ErrorMessage = "Category level is required")]
        public int CategoryLevel { get; set; }

    }
}
