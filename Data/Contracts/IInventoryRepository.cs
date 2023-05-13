using Domain.Model;
using System.Collections.Generic;

namespace Data.Contracts
{
    public interface IInventoryRepository : IGenericRepository<Domain.Model.Inventory>
    {
        ICollection<Product> GetProduct(int idProduct);
        bool RelateInventory(Product newProduct);
        bool RelateInventory(int idUser, int idInventory);
    }
}