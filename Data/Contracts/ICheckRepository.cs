﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Data.Contracts
{
    public interface ICheckRepository
    {
        Task<int> AddCheckAsync(Check entity);
        Task<bool> UpdateCheckAsync(Check entity);
        Task<bool> DeleteCheckAsync(int id);
        Task<Check> GetByIdCheckAsync(int id);
        Task<IEnumerable<Check>> GetAllAsync();
        Task<IEnumerable<Check>> GetChecksByMonthAsync(int month);
        Task<IEnumerable<Check>> GetChecksByDateAsync(DateTime date);
        Task<IEnumerable<Check>> GetChecksByMonthAndYearAsync(int month, int year);
        Task<IEnumerable<Check>> GetChecksByYearAsync(int year);
        Task AddNoteToCheckAsync(int checkId, string note);
        List<Check> GetChecksByAccountId(int accountId);
        Task<Product> GetProductByIdAsync(int productId);
    }
}