using System.Collections.Generic;
using Domain.Model;

namespace Data.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        ICollection<Account> GetAccount(int idUser);
        User Login(string username, string password);
        bool RelateInventory(int idUser, int idInventory);
        IEnumerable<User> GetAll();
    }
}
