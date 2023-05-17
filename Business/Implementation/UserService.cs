using Business.Contracts;
using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICheckRepository _checkRepository;

        public UserService(
            IUserRepository userRepository,
            IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            IAccountRepository accountRepository,
            ICheckRepository checkRepository)
        {
            _userRepository = userRepository;
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _checkRepository = checkRepository;
        }
        public bool Login(string username, string password)
        {
            // Implementación de la lógica de autenticación
            // Verificar si el usuario y la contraseña son válidos
            // Utilizar el UserRepository para realizar la verificación

            User user = (User)_userRepository.GetByUsernameAndPassword(username, password);
            return user != null;
        }
        public async Task<List<Product>> ViewInventory()
        {
            // Obtener el inventario completo utilizando el InventoryRepository

            IEnumerable<Product> products = await _inventoryRepository.GetProducts();
            List<Product> inventory = products.ToList();
            return inventory;
        }
        public async Task<bool> ModifyProduct(int productId, string productName, int quantity, decimal price, string status)
        {
            // Obtener el producto por su ID utilizando el ProductRepository
            Product product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                // El producto no existe, retornar falso o lanzar una excepción
                return false;
            }

            // Modificar las propiedades del producto
            //product.Name = productName;
            product.Quantity = quantity;
            product.Price = price;
            product.Status = status;

            // Actualizar el producto utilizando el ProductRepository
            bool result = await _productRepository.UpdateProductAsync(product);

            return result;
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
        public async Task<List<Check>> GetChecksByDate(DateTime date)
        {
            // Obtener los cortes por fecha utilizando el CheckRepository

            IEnumerable<Check> checks = await _checkRepository.GetChecksByDateAsync(date);
            List<Check> checkList = checks.ToList();
            return checkList;
        }
        public async Task<List<Check>> GetChecksByMonthAndYear(int month, int year)
        {
            // Obtener los cortes por mes y año utilizando el CheckRepository

            IEnumerable<Check> checks = await _checkRepository.GetChecksByMonthAndYearAsync(month, year);
            List<Check> checkList = checks.ToList();
            return checkList;
        }
        public async Task<List<Check>> GetChecksByYear(int year)
        {
            // Obtener los cortes por año utilizando el CheckRepository

            IEnumerable<Check> checks = await _checkRepository.GetChecksByYearAsync(year);
            List<Check> checkList = checks.ToList();
            return checkList;
        }
        public async Task<bool> AddCheck(Check check)
        {
            // Validar la lógica de negocio antes de agregar el corte
            // Verificar si el producto asociado al corte está disponible

            Product product = await _productRepository.GetProductByIdAsync(check.ProductId);
            if (product == null || product.Status != "Disponible")
            {
                return false;
            }

            // Agregar el corte utilizando el CheckRepository
            int result = await _checkRepository.AddCheckAsync(check);

            if (result > 0)
            {
                // Actualizar el estado del producto a "No Disponible"
                product.Status = "No Disponible";
                await _productRepository.UpdateProductAsync(product);
            }

            return result > 0;
        }
        public async Task<bool> UpdateCheck(Check check)
        {
            // Validar la lógica de negocio antes de actualizar el corte

            bool result = await _checkRepository.UpdateCheckAsync(check);
            return result;
        }
        public async Task<bool> DeleteCheck(int checkId)
        {
            // Validar la lógica de negocio antes de eliminar el corte

            bool result = await _checkRepository.DeleteCheckAsync(checkId);
            return result;
        }
        async Task<List<Check>> IUserService.GetChecksByMonthAsync(int month)
        {
            // Obtener los cortes por mes utilizando el CheckRepository

            IEnumerable<Check> checks = await _checkRepository.GetChecksByMonthAsync(month);
            List<Check> checkList = checks.ToList();
            return checkList;
        }
        public bool AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
