using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IAccountRepository
    {
        int Add(Account entity);
        Task<bool> Update(Account entity);
        bool Delete(int id);
        Account Get(int id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<IEnumerable<Account>> GetAllByNameAsync(string name);
    }
}