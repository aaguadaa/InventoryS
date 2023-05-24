using System.Collections.Generic;
using Domain.Model;

namespace Data.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Login(string username, string password);
        bool AddInventoryToUser(int userId, int inventoryId);
        bool AddAccountToUser(int userId, int inventoryId);
        bool AddCheckToAccount(int accountId, Check check);
        bool AddProductToInventory(int inventoryId, Product product);
        object GetByUsernameAndPassword(string username, string password);
        User GetByUsername(string username);
        bool AddNoteToCheck(int checkId, string note);
    }
}