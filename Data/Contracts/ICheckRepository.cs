using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data.Contracts
{
    public interface ICheckRepository : IGenericRepository<Check>
    {
        Task<IEnumerable<Check>> GetAllAsync();
        Task<IEnumerable<Check>> GetChecksByMonthAsync(int month);
        Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date);
        Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year);
        Task<IEnumerable<Check>> GetChecksByYearAsync(int year);
        Task AddNoteToCheckAsync(int checkId, string note);
        List<Check> GetChecksByAccountId(int accountId);
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductToCheckAsync(int checkId, Product product);
        Task UpdateProductStatusAsync(int productId, string status);
    }
}