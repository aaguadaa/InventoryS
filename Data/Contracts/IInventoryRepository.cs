using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IInventoryRepository : IGenericRepository<Domain.Model.Inventory>
    {
        ICollection<Product> GetProduct(int idProduct);
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> UpdateProductAsync(Product product);
        bool RelateInventory(Product newProduct);
        bool RelateInventory(int idUser, int idInventory);
        Task<IEnumerable<Product>> GetProducts();
        Task<bool> DeleteProduct(int productId);
        Task<bool> AddProduct(Product product);
    }
}