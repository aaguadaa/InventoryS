using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IAccountService
    {
        List<Account> GetAccountsByDate(DateTime date);
        List<Account> GetAccountsByMonth(int month, int year);
        List<Account> GetAccountsByYear(int year);
        Account GetAccountById(int accountId);
        bool AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(int accountId);
        List<Check> GetChecksByAccountId(int accountId);
        Task<bool> AddCheck(Check check);
        Task<bool> UpdateCheck(Check check);
        Task<bool> DeleteCheck(int checkId);
    }
}