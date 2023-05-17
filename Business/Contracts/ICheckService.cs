using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Business.Contracts
{
    public interface ICheckService
    {
        Task<int> AddCheckAsync(Check check);
        Task<bool> UpdateCheckAsync(Check check);
        Task<bool> DeleteCheckAsync(int checkId);
        Task<Check> GetCheckByIdAsync(int checkId);
        Task<IEnumerable<Check>> GetAllChecksAsync();
        Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date);
        Task<IEnumerable<Check>> GetChecksByMonthAsync(int month);
        Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year);
        Task<IEnumerable<Check>> GetChecksByYearAsync(int year);
        Task AddNoteToCheckAsync(int checkId, string note);
        List<Check> GetChecksByAccountId(int accountId);
        Task<Product> GetProductByIdAsync(int productId);
    }
}