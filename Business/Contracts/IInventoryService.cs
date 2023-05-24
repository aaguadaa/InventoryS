using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Business.Contracts
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetInventory();
        Task<Product> GetProductById(int id);
        Task<int> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        int Add(Domain.Model.Inventory l);
        bool Update(Domain.Model.Inventory l);
        Domain.Model.Inventory Get(int id);
        bool Delete(int id);
    }
}