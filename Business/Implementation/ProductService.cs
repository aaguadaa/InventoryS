using Business.Contracts;
using Data.Contracts;
using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _productRepository.GetAvailableProductsAsync();
        }
        public async Task<IEnumerable<Product>> GetUnavailableProductsAsync()
        {
            return await _productRepository.GetUnavailableProductsAsync();
        }
    }
}