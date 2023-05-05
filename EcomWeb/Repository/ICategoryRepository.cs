using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Category GetCategoryByName(string categoryName);
    }
}
