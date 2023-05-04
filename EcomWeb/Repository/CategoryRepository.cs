using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MyDbContext context) : base(context) { }
        public Category GetCategoryByName(string categoryName)
        {
            return GetByCondition(b=>b.CategoryName == categoryName).FirstOrDefault();
        }
    }
}
