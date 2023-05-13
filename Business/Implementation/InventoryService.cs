using Business.Contracts;
using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepo;
        public InventoryService(IInventoryRepository projectRepo)
        {
            _inventoryRepo = projectRepo;
        }
        public int Add(Domain.Model.Inventory l)
        {
            if (l.Id <= 0) return 0;
            if (string.IsNullOrEmpty(l.Categoria)) return 0;
            if (string.IsNullOrEmpty(l.Description)) return 0;
            return _inventoryRepo.Add(l);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;
            return (_inventoryRepo.Delete(id));
        }

        public Domain.Model.Inventory Get(int id)
        {
            Domain.Model.Inventory l = _inventoryRepo.Get(id);
            return l;
        }

        public bool Update(Domain.Model.Inventory l)
        {
            if (l.Id <= 0) return false;
            if (string.IsNullOrEmpty(l.Categoria)) return false;
            if (string.IsNullOrEmpty(l.Description)) return false;
            return _inventoryRepo.Update(l);
        }

        public bool RelateProduct(Domain.Model.Product newProduct)
        {
            throw new NotImplementedException();
        }
    }
}
