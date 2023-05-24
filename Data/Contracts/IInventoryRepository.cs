using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IInventoryRepository : IGenericRepository<Domain.Model.Inventory>
    {
        IEnumerable<Inventory> GetAllInventories();
        IEnumerable<Product> GetProductsInInventory(int inventoryId);
        void AddProductToInventory(int inventoryId, Product product);
        void RemoveProductFromInventory(int inventoryId, int productId);
    }
}