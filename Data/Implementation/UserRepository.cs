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
        public ICollection<Account> GetAccounts(int idUser)
        {
            if (idUser <= 0) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                var userAccounts = ctx.Checks.Where(up => up.User.Id == idUser)
                                    .Include(up => up.Account).Select(up => up.Account).ToList();

                return userAccounts;
            }
        }
        public bool RelateAccount(int idUser, int idAccount)
        {
            if (idUser <= 0) return false;
            if (idAccount <= 0) return false;
            using (var ctx = new InventoryStevDBContext())
            {
                //Obtenemos el usuario y la cuenta a relacionar
                var user = ctx.Users.SingleOrDefault(x => x.Id == idUser);
                var account = ctx.Accounts.SingleOrDefault(x => x.Id == idAccount);
                //validamos si existe
                if (user == null || account == null) return false;

                var existingRelation = ctx.Checks.SingleOrDefault(up => up.User.Id == idUser && up.Account.Id == idAccount);
                if (existingRelation != null) return true; // checamos si ya existe la relacion y la validamos

                //Creamos el nuevo objeto
                var userAccount = new Check { User = user, Account = account };
                ctx.Checks.Add(userAccount);
                ctx.SaveChanges();
            }
            return true;
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
            if (username == null || password == null) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
                return currentUser;
            }

        }
        public ICollection<Account> GetAccount(int idUser)
        {
            if (idUser <= 0) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                var userInventory = ctx.Checks.Where(up => up.User.Id == idUser)
                                    .Include(up => up.Account).Select(up => up.Account).ToList();

                return userInventory;
            }
        }
        public bool RelateInventory(int idUser, int idInventory)
        {
            if (idUser <= 0) return false;
            if (idInventory <= 0) return false;
            using (var ctx = new InventoryStevDBContext())
            {
                //Obtenemos elusuario y el proyecto a relacionar
                var user = ctx.Users.SingleOrDefault(x => x.Id == idUser);
                var inventory = ctx.Accounts.SingleOrDefault(x => x.Id == idInventory);
                //validamos si existe
                if (user == null || inventory == null) return false;

                var existingRelation = ctx.Checks.SingleOrDefault(up => up.User.Id == idUser && up.Account.Id == idInventory);
                if (existingRelation != null) return true; // checamos si ya existe la relacion y la validamos

                //Creamos el nuevo objeto
                var userInventory = new Check { User = user, Account = inventory };
                ctx.Checks.Add(userInventory);
                ctx.SaveChanges();
            }
            return true;
        }
        public IEnumerable<User> GetAll()
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return ctx.Users.ToList();
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
            throw new NotImplementedException();
        }

        public object GetByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
