﻿using AutoMapper;
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

        public async Task<Category> Create(Category category)
        {
            if (GetCategoryByName(category.CategoryName).Result != null)
                throw new Exception("This category name already exists.");

            _categoryRepository.Create(category);
            return category;
        }

        public async Task<bool> Delete(Category category)
        {
            if (GetById(category.CategoryId).Result == null)
                throw new Exception("This category doesn't exists.");

            _categoryRepository.Delete(category);
            return true;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _categoryRepository.GetCategoryByName(categoryName);
        }

        public async Task<Category> Update(Category category)
        {
            if (GetById(category.CategoryId).Result == null)
                throw new Exception("This category doesn't exists.");

            await _categoryRepository.Update(category);
            return category;
        }
    }
}
