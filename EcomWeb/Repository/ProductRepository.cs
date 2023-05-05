using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(b => b.Category).ToListAsync();
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
    }
}
