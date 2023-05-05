using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<bool> Delete(Category category);
        Task<Category> GetCategoryByName(string categoryName);
    }
}
