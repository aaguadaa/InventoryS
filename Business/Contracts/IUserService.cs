using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IUserService
    {
        bool Login(string username, string password);
        bool AddProduct(Product product);
        Task<List<Product>> ViewInventory();
        Task<bool> ModifyProduct(int productId, string productName, int quantity, decimal price, string status);
        List<Account> GetAccountsByDate(DateTime date);
        List<Account> GetAccountsByMonth(int month, int year);
        List<Account> GetAccountsByYear(int year);
        Account GetAccountById(int accountId);
        Task<List<Check>> GetChecksByDate(DateTime date);
        Task<List<Check>> GetChecksByMonthAsync(int month);
        Task<List<Check>> GetChecksByMonthAndYear(int month, int year);
        Task<List<Check>> GetChecksByYear(int year);
        Task<bool> AddCheck(Check check);
        Task<bool> UpdateCheck(Check check);
        Task<bool> DeleteCheck(int checkId);
    }
}