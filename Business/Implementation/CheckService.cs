using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Contracts;
using Data.Contracts;
using Domain.Model;

namespace Business.Services
{
    public class CheckService : ICheckService
    {
        private readonly ICheckRepository _checkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public CheckService(ICheckRepository checkRepository, IAccountRepository accountRepository, IInventoryRepository inventoryRepository)
        {
            _checkRepository = checkRepository;
            _accountRepository = accountRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<int> AddCheckAsync(Check check)
        {
            // Modificar el estado del producto en el inventario
            await SetProductStatusUnavailableAsync(check.Product.Id);

            return await _checkRepository.AddCheckAsync(check);
        }
        public async Task<bool> UpdateCheckAsync(Check check)
        {
            // Modificar el estado del producto en el inventario si es necesario
            if (check.Product != null && check.Product.Status == "Disponible")
            {
                await SetProductStatusUnavailableAsync(check.Product.Id);
            }

            return await _checkRepository.UpdateCheckAsync(check);
        }
        public async Task<bool> DeleteCheckAsync(int checkId)
        {
            var check = await _checkRepository.GetByIdCheckAsync(checkId);
            if (check != null)
            {
                // Restaurar el estado del producto en el inventario
                await SetProductStatusAvailableAsync(check.Product.Id);

                return await _checkRepository.DeleteCheckAsync(checkId);
            }
            return false;
        }
        public async Task<Check> GetCheckByIdAsync(int checkId)
        {
            return await _checkRepository.GetByIdCheckAsync(checkId);
        }
        public async Task<IEnumerable<Check>> GetAllChecksAsync()
        {
            return await _checkRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date)
        {
            return await _checkRepository.GetChecksByDateAsync(date);
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year)
        {
            return await _checkRepository.GetChecksByMonthAndYearAsync(month, year);
        }
        public async Task<IEnumerable<Check>> GetChecksByYearAsync(int year)
        {
            return await _checkRepository.GetChecksByYearAsync(year);
        }
        public async Task AddNoteToCheckAsync(int checkId, string note)
        {
            await _checkRepository.AddNoteToCheckAsync(checkId, note);
        }
        private async Task SetProductStatusUnavailableAsync(int productId)
        {
            var product = await _inventoryRepository.GetProductByIdAsync(productId);
            if (product != null)
            {
                product.Status = "No Disponible";
                await _inventoryRepository.UpdateProductAsync(product);
            }
        }
        private async Task SetProductStatusAvailableAsync(int productId)
        {
            var product = await _inventoryRepository.GetProductByIdAsync(productId);
            if (product != null)
            {
                product.Status = "Disponible";
                await _inventoryRepository.UpdateProductAsync(product);
            }
        }
        public List<Check> GetChecksByAccountId(int accountId)
        {
            return _checkRepository.GetChecksByAccountId(accountId);
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _inventoryRepository.GetProductByIdAsync(productId);
        }
        public async Task<IEnumerable<Check>> GetChecksByMonthAsync(int month)
        {
            // Obtener los cortes por mes utilizando el repositorio
            return await _checkRepository.GetChecksByMonthAsync(month);
        }
    }
}