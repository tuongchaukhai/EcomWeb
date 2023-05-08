using EcomWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAll(int page = 1, int pageSize = 10)
        {
            return await _context.Products.Include(b => b.Category).Skip((page - 1) * pageSize).ToListAsync();
        }

        public async Task<Product> GetByTitle(string title)
        {
            return await _context.Products
                .Include(b => b.Category)
                .Where(b => b.ProductName == title).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            return await _context.Products
                .Include(b => b.Category)
                .Where(b => b.Category.CategoryName == categoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByPriceRange(double min, double max)
        {
            return await GetByCondition(b => b.Price >= min && b.Price <= max);
        }

        public async Task<IEnumerable<Product>> Filter([FromQuery] string? title = null, string? categoryName = null, double minPrice = 0, double maxPrice = double.MaxValue, string? sortBy = null, int page = 1, int pageSize = 10)
        {
            var products = _context.Products.Include(b => b.Category).AsQueryable();

            //Filtering
            if (!string.IsNullOrEmpty(title))
            {
                products = products.Where(b => b.ProductName.Contains(title));
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                products = products.Where(b => b.Category.CategoryName == categoryName);
            }

            products.Where(b => b.Price >= minPrice && b.Price <= maxPrice);

            //Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "title_asc": products = products.OrderBy(b => b.ProductName); break;
                    case "title_desc": products = products.OrderByDescending(b => b.ProductName); break;
                    case "price_asc": products.OrderBy(b => b.Price); break;
                    case "price_desc": products.OrderByDescending(b => b.Price); break;

                        //sold value

                }
            }

            //Paging
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            return await products.ToListAsync();
        }


    }
}
