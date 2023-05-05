using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MyDbContext context) : base(context) { }
        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _context.Categories.Where(b=>b.CategoryName == categoryName).FirstOrDefaultAsync();
        }
    }
}
