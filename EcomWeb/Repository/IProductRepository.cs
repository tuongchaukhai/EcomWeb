using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcomWeb.Repository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        new Task<ProductsPage> GetAll(int page = 1, int pageSize = 10);
        Task<IEnumerable<Product>> ExportData();

        Task<Product> GetByTitle(string title);

        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByPriceRange(double min, double max);

        Task<IEnumerable<Product>> Filter([FromQuery] string? title = null, [FromQuery] string? categoryName = null,  double minPrice = 0, double maxPrice = double.MaxValue, string? sortBy = null, int page = 1, int pageSize = 10);
    }
}
