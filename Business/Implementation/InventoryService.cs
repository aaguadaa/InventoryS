using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Contracts;
using Data.Contracts;
using Data.Implementation;
using Domain.Model;

namespace Business.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICheckRepository _checkRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IProductRepository productRepository, ICheckRepository checkRepository, IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _checkRepository = checkRepository;
            _inventoryRepository = inventoryRepository;

        }
        public async Task<IEnumerable<Product>> GetInventory()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<int> AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            // Realizar otras validaciones u operaciones de negocio antes de guardar el producto

            product.Status = "Disponible";
            return await _productRepository.AddProductAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            // Realizar otras validaciones u operaciones de negocio antes de actualizar el producto

            return await _productRepository.UpdateProductAsync(product);
        }
        public async Task<bool> DeleteProduct(int id)
        {
            // Realizar otras validaciones u operaciones de negocio antes de eliminar el producto

            return await _productRepository.DeleteProductAsync(id);
        }
        public async Task<IEnumerable<Check>> GetChecksByAccountId(int accountId)
        {
            var checks = _checkRepository.GetChecksByAccountId(accountId);
            return await Task.FromResult(checks);
        }

        public async Task<int> AddCheck(Check check)
        {
            if (check == null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            // Realizar otras validaciones u operaciones de negocio antes de guardar el corte del día

            // Modificar el estado del producto guardado en el inventario a "No Disponible"
            var product = await _productRepository.GetProductByIdAsync(check.Product.Id);
            if (product != null)
            {
                product.Status = "No Disponible";
                await _productRepository.UpdateProductAsync(product);
            }

            return await _checkRepository.AddCheckAsync(check);
        }
        public async Task<bool> AddNoteToCheck(int checkId, string note)
        {
            if (string.IsNullOrEmpty(note))
            {
                throw new ArgumentNullException(nameof(note));
            }

            var check = await _checkRepository.GetByIdCheckAsync(checkId);
            if (check != null)
            {
                check.Notes.Add(note);
                await _checkRepository.UpdateCheckAsync(check);
                return true;
            }

            return false;
        }

        public int Add(Domain.Model.Inventory inventory)
        {
            if (inventory == null)
            {
                throw new ArgumentNullException(nameof(inventory));
            }

            if (_inventoryRepository.InventoryExists(inventory.Id))
            {
                throw new InvalidOperationException("El inventario ya existe.");
            }

            return _inventoryRepository.Add(inventory);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Model.Inventory Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Domain.Model.Inventory l)
        {
            throw new NotImplementedException();
        }
    }
}