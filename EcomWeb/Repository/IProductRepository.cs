using EcomWeb.Models;
using System.Diagnostics.Eventing.Reader;

namespace EcomWeb.Repository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        new IQueryable<Product> GetAll();
        Product GetByTitle(string title);

        IQueryable<Product> GetByCategory(string categoryName);

        IQueryable<Product> GetByPriceRange(double min, double max);
    }
}
