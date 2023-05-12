using EcomWeb.Dtos.Product;
using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface IProductService
    {
        Task<ProductsPage> GetAll(int page = 1, int pageSize = 10);
        Task<IEnumerable<Product>> ExportData();

        Task<Product> GetById(int id);
        Task<Product> GetByTitle(string title);
        Task<IEnumerable<Product>> GetByPriceRange(double min, double max);
        Task<IEnumerable<Product>> GetByCategory(string categoryName);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(Product product);
    }
}
