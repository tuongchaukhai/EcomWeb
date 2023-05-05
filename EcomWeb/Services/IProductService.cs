using EcomWeb.Models;
using EcomWeb.Repository;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace EcomWeb.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> GetByTitle(string title);
        Task<IEnumerable<Product>> GetByPriceRange(double min, double max);
        Task<IEnumerable<Product>> GetByCategory(string categoryName);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(Product product);
    }
}
