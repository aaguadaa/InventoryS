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

namespace Data.Implementation
{
    public class UserRepository : IUserRepository
    {
        public int Add(User entity)
        {
            if (entity == null) return 0;
           // entity.CreatedDate = DateTime.Now;
          //  entity.ModifiedDate = null;
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
        ICollection<Account> IUserRepository.GetAccount(int idUser)
        {
            if (idUser <= 0) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                var userInventory = ctx.Checks.Where(up => up.User.Id == idUser)
                                    .Include(up => up.account).Select(up => up.account).ToList();

                return (ICollection<Account>)userInventory;
            }
        }

        bool IUserRepository.RelateInventory(int idUser, int idInventory)
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

                var existingRelation = ctx.Checks.SingleOrDefault(up => up.User.Id == idUser && up.account.Id == idInventory);
                if (existingRelation != null) return true; // checamos si ya existe la relacion y la validamos

                //Creamos el nuevo objeto
                var userInventory = new Check { User = user, account = inventory };
                ctx.Checks.Add(userInventory);
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

        User IUserRepository.Login(string username, string password)
        {
            if (username == null || password == null) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                User currentUser = ctx.Users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();
                return currentUser;
            }

        }
    }
}