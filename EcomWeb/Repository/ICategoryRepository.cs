using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<Category> GetCategoryByName(string categoryName);
    }
}
