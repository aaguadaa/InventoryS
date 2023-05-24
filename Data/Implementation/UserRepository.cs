using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Domain;
using Domain.Model;
using System.Linq.Expressions;

namespace Data.Implementation
{
    public class UserRepository : IUserRepository
    {
        public int Add(User entity)
        {
            if (entity == null) return 0;
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Users.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }
        public bool Delete(int id)
        {
            if (id <= 0) return false;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.SingleOrDefault(x => x.Id == id);
                if (currentUser == null) return false;
                ctx.Users.Remove(currentUser);
                ctx.SaveChanges();
                return true;
            }
        }
        public User Get(int id)
        {
            if (id <= 0) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.SingleOrDefault(x => x.Id == id);
                return currentUser;
            }
        }
        public bool Update(User entity)
        {
            if (entity == null) return false;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.SingleOrDefault(x => x.Id == entity.Id);
                if (currentUser == null) return false;
                currentUser.Name = entity.Name;
                currentUser.UserName = entity.UserName;
                currentUser.Password = entity.Password;

                ctx.SaveChanges();
                return true;
            }
        }
        public User Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
                return currentUser;
            }
        }
        public User GetById(int id, params Expression<Func<User, object>>[] includeProperties)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<User> query = ctx.Users;
                foreach (Expression<Func<User, object>> includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.FirstOrDefault(x => x.Id == id);
            }
        }
        public IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "")
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<User> query = ctx.Users;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }
        public User GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.FirstOrDefault(u => u.UserName == username);
                return currentUser;
            }
        }
        public object GetByUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                var currentUser = ctx.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
                return currentUser;
            }
        }
        public bool AddInventoryToUser(int userId, int inventoryId)
        {
            if (userId <= 0 || inventoryId <= 0) return false;

            using (var ctx = new InventoryStevDBContext())
            {
                var user = ctx.Users.Include(u => u.Inventory).SingleOrDefault(u => u.Id == userId);
                var inventory = ctx.Inventories.SingleOrDefault(i => i.Id == inventoryId);

                if (user == null || inventory == null) return false;

                user.Inventory = inventory;
                ctx.SaveChanges();

                return true;
            }
        }
        public bool AddAccountToUser(int userId, int accountId)
        {
            if (userId <= 0 || accountId <= 0) return false;

            using (var ctx = new InventoryStevDBContext())
            {
                var user = ctx.Users.SingleOrDefault(u => u.Id == userId);
                var account = ctx.Accounts.SingleOrDefault(a => a.Id == accountId);

                if (user == null || account == null) return false;

                user.Accounts.Add(account);
                ctx.SaveChanges();
            }

            return true;
        }
        public bool AddCheckToAccount(int accountId, Check check)
        {
            if (accountId <= 0 || check == null) return false;

            using (var ctx = new InventoryStevDBContext())
            {
                var account = ctx.Accounts.Include(a => a.Checks).SingleOrDefault(a => a.Id == accountId);

                if (account == null) return false;

                account.Checks.Add(check);
                ctx.SaveChanges();
            }

            return true;
        }
        public bool AddProductToInventory(int inventoryId, Product product)
        {
            if (inventoryId <= 0 || product == null) return false;

            using (var ctx = new InventoryStevDBContext())
            {
                var inventory = ctx.Inventories.Include(i => i.Products).SingleOrDefault(i => i.Id == inventoryId);

                if (inventory == null) return false;

                inventory.Products.Add(product);
                ctx.SaveChanges();
            }

            return true;
        }
        public bool AddNoteToCheck(int checkId, string note)
        {
            if (checkId <= 0 || string.IsNullOrEmpty(note)) return false;

            using (var ctx = new InventoryStevDBContext())
            {
                var check = ctx.Checks.SingleOrDefault(c => c.Id == checkId);

                if (check == null) return false;

                check.Notes.Add(note);
                ctx.SaveChanges();
            }

            return true;
        }
    }
}