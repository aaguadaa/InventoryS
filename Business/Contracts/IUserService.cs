using Domain.Model;
using System.Collections.Generic;
namespace Business.Contracts
{
    public interface IUserService
    {
        bool Login(string username, string password);
        List<Product> ViewInventory();
        bool ModifyProduct(int productId, string productName, int quantity, decimal price);
        bool AddProduct(Product product);
        List<Account> GetAccountsByDate(string date);
        bool AddAccount(Account account);
    }
}
