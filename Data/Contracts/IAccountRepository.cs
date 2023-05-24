using Domain.Model;
using System;
using System.Collections.Generic;

namespace Data.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        List<Account> GetAccountsByDate(DateTime date);
        List<Account> GetAccountsByMonth(int month, int year);
        List<Account> GetAccountsByYear(int year);
    }
}