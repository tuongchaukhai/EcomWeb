using AutoMapper;
using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category Create(Category category)
        {
            if (GetCategoryByName(category.CategoryName) != null)
                return null;

            _categoryRepository.Create(category);
            return category;
        }

        public bool Delete(Category category)
        {
            if (GetById(category.CategoryId) == null)
                return false;

            _categoryRepository.Delete(category);
            return true;
        }

        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryRepository.GetCategoryByName(categoryName);
        }

        public Category Update(Category category)
        {
            if (GetById(category.CategoryId) == null)
                return null;

            _categoryRepository.Update(category);
            return category;
        }
    }
}
