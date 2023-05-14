using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data.Contracts
{
    public interface ICheckRepository : IGenericRepository<Check>
    {
        Task<IEnumerable<Check>> GetAllByDateAsync(DateTime date);
        Task<IEnumerable<Check>> GetAllByUserIdAsync(int userId);
    }
}