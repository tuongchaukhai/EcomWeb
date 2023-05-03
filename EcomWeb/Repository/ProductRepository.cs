using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public override IQueryable<Product> GetAll()
        {
            return _context.Products.Include(b => b.Category);
        }

        public Product GetByTitle(string title)
        {
            return GetByCondition(b => b.ProductName == title).FirstOrDefault();
        }

        public IQueryable<Product> GetByCategory(string categoryName)
        {

            return _context.Products
                .Include(b => b.Category)
                .Where(b => b.Category.CategoryName == categoryName);
        }

        public IQueryable<Product> GetByPriceRange(double min, double max)
        {
            return GetByCondition(b => b.Price >= min && b.Price <= max);
        }
    }
}
