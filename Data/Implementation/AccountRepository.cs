using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Data.Contracts;
using Domain.Model;
using System.Linq.Expressions;

namespace Data.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        public int Add(Account entity)
        {
            using (var context = new InventoryStevDBContext())
            {
                context.Accounts.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }
        public Account Get(int id)
        {
            using (var context = new InventoryStevDBContext())
            {
                return context.Accounts.FirstOrDefault(a => a.Id == id);
            }
        }
        public Account GetById(int id, params Expression<Func<Account, object>>[] includeProperties)
        {
            using (var context = new InventoryStevDBContext())
            {
                IQueryable<Account> query = context.Accounts;
                foreach (Expression<Func<Account, object>> includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault(a => a.Id == id);
            }
        }
        public bool Update(Account entity)
        {
            using (var context = new InventoryStevDBContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
        public List<Account> GetAccountsByDate(DateTime date)
        {
            using (var context = new InventoryStevDBContext())
            {
                return context.Accounts.Where(a => a.Date.Date == date.Date).ToList();
            }
        }
        public List<Account> GetAccountsByMonth(int month, int year)
        {
            using (var context = new InventoryStevDBContext())
            {
                return context.Accounts
                    .Where(a => a.Date.Month == month && a.Date.Year == year)
                    .ToList();
            }
        }
        public List<Account> GetAccountsByYear(int year)
        {
            using (var context = new InventoryStevDBContext())
            {
                return context.Accounts.Where(a => a.Date.Year == year).ToList();
            }
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Account> GetAll(Expression<Func<Account, bool>> filter = null, Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = null, string includeProperties = "")
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<Account> query = ctx.Accounts;

                // Aplicar filtros
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Incluir propiedades relacionadas
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                // Aplicar ordenamiento
                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                return query.ToList();
            }
        }
    }
}