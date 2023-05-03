using EcomWeb.Models;
using EcomWeb.Repository;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace EcomWeb.Services
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        Product GetById(int id);
        Product GetByTitle(string title);
        IQueryable<Product> GetByPriceRange(double min, double max);
        IQueryable<Product> GetByCategory(string categoryName);
        Product Create(Product product);
        Product Update(Product product);
        bool Delete(Product product);
    }
}
