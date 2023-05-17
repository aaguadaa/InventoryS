using Business.Contracts;
using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICheckRepository _checkRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public AccountService(
            IAccountRepository accountRepository,
            ICheckRepository checkRepository,
            IInventoryRepository inventoryRepository)
        {
            _accountRepository = accountRepository;
            _checkRepository = checkRepository;
            _inventoryRepository = inventoryRepository;
        }
        public List<Account> GetAccountsByDate(DateTime date)
        {
            // Obtener las cuentas por fecha utilizando el AccountRepository

            List<Account> accounts = _accountRepository.GetAccountsByDate(date);
            return accounts;
        }
        public List<Account> GetAccountsByMonth(int month, int year)
        {
            // Obtener las cuentas por mes y año utilizando el AccountRepository

            List<Account> accounts = _accountRepository.GetAccountsByMonth(month, year);
            return accounts;
        }
        public List<Account> GetAccountsByYear(int year)
        {
            // Obtener las cuentas por año utilizando el AccountRepository

            List<Account> accounts = _accountRepository.GetAccountsByYear(year);
            return accounts;
        }
        public Account GetAccountById(int accountId)
        {
            // Obtener una cuenta por su ID utilizando el AccountRepository

            Account account = _accountRepository.GetAccountById(accountId);
            return account;
        }
        public bool AddAccount(Account account)
        {

            bool result = _accountRepository.AddAccount(account);
            return result;
        }
        public bool UpdateAccount(Account account)
        {
            bool result = _accountRepository.UpdateAccount(account);
            return result;
        }
        public bool DeleteAccount(int accountId)
        {
            // Validar la lógica de negocio antes de eliminar la cuenta

            bool result = _accountRepository.DeleteAccount(accountId);
            return result;
        }
        public List<Check> GetChecksByAccountId(int accountId)
        {
            // Obtener los cortes asociados a una cuenta utilizando el CheckRepository

            List<Check> checks = _checkRepository.GetChecksByAccountId(accountId);
            return checks;
        }
        public async Task<bool> AddCheck(Check check)
        {

            Product product = await _inventoryRepository.GetProductByIdAsync(check.ProductId);
            if (product == null || product.Status != "Disponible")
            {
                return false;
            }

            // Agregar el corte utilizando el CheckRepository
            bool result = await _checkRepository.AddCheckAsync(check) > 0;

            if (result)
            {
                // Actualizar el estado del producto a "No Disponible"
                product.Status = "No Disponible";
                await _inventoryRepository.UpdateProductAsync(product);
            }

            return result;
        }
        public async Task<bool> UpdateCheck(Check check)
        {
            bool result = await _checkRepository.UpdateCheckAsync(check);
            return result;
        }
        public async Task<bool> DeleteCheck(int checkId)
        {
            bool result = await _checkRepository.DeleteCheckAsync(checkId);
            return result;
        }
    }
}
