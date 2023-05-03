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

        public Product Create(Product product)
        {
            if (GetByTitle(product.ProductName) != null)
                return null;

            product.CreatedDate = DateTime.Now;
            _productRepository.Create(product);
            return product;
        }

        public bool Delete(Product product)
        {
            if (GetById(product.ProductId) == null)
                return false;
            
            _productRepository.Delete(product);
            return true;
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IQueryable<Product> GetByCategory(string categoryName)
        {
            return _productRepository.GetByCategory(categoryName);
        }


        public IQueryable<Product> GetByPriceRange(double min, double max)
        {
            return _productRepository.GetByPriceRange(min, max);
        }

        public Product GetByTitle(string title)
        {
            return _productRepository.GetByTitle(title);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Update(Product product)
        {
            if (GetById(product.ProductId) == null)
                return null;

            product.LastModified = DateTime.Now;
            _productRepository.Update(product);
            return product;
        }
    }
}
