using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Create(Product product)
        {
            if (GetByTitle(product.ProductName).Result != null)
                throw new Exception("This product title already exists.");

            await _productRepository.Create(product);
            return product;
        }

        public async Task<bool> Delete(Product product)
        {
            if (GetById(product.ProductId).Result == null)
                throw new Exception("This product doesn't exists.");

            await _productRepository.Delete(product);
            return true;
        }

        public async Task<ProductsPage> GetAll(int page = 1, int pageSize = 10)
        {
            return await _productRepository.GetAll(page,pageSize);
        }

        public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
        {
            return await _productRepository.GetByCategory(categoryName);
        }

        public async Task<IEnumerable<Product>> GetByPriceRange(double min, double max)
        {
            return await _productRepository.GetByPriceRange(min, max);
        }

        public async Task<Product> GetByTitle(string title)
        {
            return await _productRepository.GetByTitle(title);
        }

        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product> Update(Product product)
        {
            if (GetById(product.ProductId).Result == null)
                throw new Exception("This product doesn't exists.");

            await _productRepository.Update(product);
            return product;
        }
    }
}
