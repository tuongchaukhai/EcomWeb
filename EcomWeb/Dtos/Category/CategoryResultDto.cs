namespace EcomWeb.Dtos.Category
{
    public class CategoryResultDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public string? ParentCategory { get; set; }

        public string? Cover { get; set; }

        public int CategoryLevel { get; set; }

    }
}
