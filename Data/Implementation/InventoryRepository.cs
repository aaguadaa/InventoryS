using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        public int Add(Domain.Model.Inventory entity)
        {
            if (entity == null) return 0;
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Inventories.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }

        }
        public bool Delete(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {

                Domain.Model.Inventory l = ctx.Inventories.SingleOrDefault(p => p.Id == id);
                if (l == null) return false;

                ctx.Inventories.Remove(l);

                ctx.SaveChanges();
                return true;
            }
        }

        public Domain.Model.Inventory Get(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                Domain.Model.Inventory l = ctx.Inventories.SingleOrDefault(p => p.Id == id);
                if (l == null) return null;
                return l;
            }
        }

        public ICollection<Product> GetProduct(int idProduct)
        {
            if (idProduct <= 0) return null;
            using (var ctx = new InventoryStevDBContext())
            {
                var userInventory = ctx.Checks.Where(up => up.User.Id == idProduct)
                                    .Include(up => up.account).Select(up => up.account).ToList();

                return (ICollection<Product>)userInventory;
            }
        }

        bool IInventoryRepository.RelateInventory(int idUser, int idInventory)
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
        public bool Update(Domain.Model.Inventory entity)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                Domain.Model.Inventory l = ctx.Inventories.SingleOrDefault(x => x.Id == entity.Id);
                l.product = entity.product;

                ctx.SaveChanges();
                return true;
            }
        }

        public bool RelateInventory(Product newProduct)
        {
            throw new NotImplementedException();
        }
    }
}