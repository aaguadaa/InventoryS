using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly InventoryStevDBContext _dbContext;
        public AccountRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Account> GetAccountsByDate(DateTime date)
        {
            // Obtener las cuentas por fecha utilizando Entity Framework

            return _dbContext.Accounts.Where(a => DbFunctions.DiffDays(a.Date, date) == 0).ToList();
        }
        public List<Account> GetAccountsByMonth(int month, int year)
        {
            // Obtener las cuentas por mes y año utilizando Entity Framework

            return _dbContext.Accounts.Where(a => a.Date.Month == month && a.Date.Year == year).ToList();
        }
        public List<Account> GetAccountsByYear(int year)
        {
            // Obtener las cuentas por año utilizando Entity Framework

            return _dbContext.Accounts.Where(a => a.Date.Year == year).ToList();
        }
        public Account GetAccountById(int accountId)
        {
            // Obtener una cuenta por su Id utilizando Entity Framework

            return _dbContext.Accounts.Find(accountId);
        }
        public bool AddAccount(Account account)
        {
            // Agregar una cuenta utilizando Entity Framework

            _dbContext.Accounts.Add(account);
            return _dbContext.SaveChanges() > 0;
        }
        public bool UpdateAccount(Account account)
        {
            // Actualizar una cuenta utilizando Entity Framework

            _dbContext.Entry(account).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }
        public bool DeleteAccount(int accountId)
        {
            // Eliminar una cuenta utilizando Entity Framework

            var account = _dbContext.Accounts.Find(accountId);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
        public bool AddNoteToAccount(int accountId, string note)
        {
            // Agregar una nota a una cuenta utilizando Entity Framework

            var account = _dbContext.Accounts.Find(accountId);
            if (account != null)
            {
                account.Notes.Add(note);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
    }
}