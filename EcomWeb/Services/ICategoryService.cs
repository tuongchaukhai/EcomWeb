using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Category GetById(int id);
        Category Create(Category category);
        Category Update(Category category);
        bool Delete(Category category);
        Category GetCategoryByName(string categoryName);
    }
}
