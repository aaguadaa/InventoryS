using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        IEnumerable<Inventory> GetAllInventories();
        IEnumerable<Product> GetProductsInInventory(int inventoryId);
        Task AddProductToInventoryAsync(int inventoryId, Product product);
        Task RemoveProductFromInventoryAsync(int inventoryId, int productId);
        Task UpdateStatusProductAsync(int productId, string status);
    }
}