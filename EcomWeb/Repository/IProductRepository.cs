using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        new Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByTitle(string title);

        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task<IEnumerable<Product>> GetByPriceRange(double min, double max);
    }
}
