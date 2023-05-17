using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IProductService
    {
        Task<int> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAvailableProductsAsync();
        Task<IEnumerable<Product>> GetUnavailableProductsAsync();
    }
}