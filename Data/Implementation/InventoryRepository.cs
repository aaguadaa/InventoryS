using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryStevDBContext _dbContext;

        public InventoryRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Domain.Model.Inventory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (InventoryExists(entity.Id))
            {
                throw new InvalidOperationException("El inventario ya existe.");
            }

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
            using (var ctx = new InventoryStevDBContext())
            {
                var products = (from c in ctx.Checks
                                join a in ctx.Accounts on c.Account.Id equals a.Id
                                join i in ctx.Inventories on a.Inventory.Id equals i.Id
                                join p in ctx.Products on c.Product.Id equals p.Id
                                where c.User.Id == idProduct
                                select p).ToList();

                return products;
            }
        }
        bool IInventoryRepository.RelateInventory(int idUser, int idInventory)
        {
            if (idUser <= 0) return false;
            if (idInventory <= 0) return false;
            using (var ctx = new InventoryStevDBContext())
            {
                //Obtenemos el usuario y el proyecto a relacionar
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
        public bool Update(Domain.Model.Inventory entity)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var l = ctx.Inventories.FirstOrDefault(x => x.Id == entity.Id);
                if (l == null) return false;

                l.Categoria = entity.Categoria;
                l.Description = entity.Description;
                l.CreatedDate = entity.CreatedDate;
                l.ModifiedDate = DateTime.Now;
                l.Account = entity.Account;
                l.UpdatedProduct = entity.UpdatedProduct; // aquí se cambia product por UpdatedProduct
                l.Products = entity.Products;

                ctx.SaveChanges();
                return true;
            }
        }
        public bool RelateInventory(Product newProduct)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var inventories = ctx.Inventories.Include(i => i.Products).ToList();
                foreach (var inventory in inventories)
                {
                    var existingProduct = inventory.Products.FirstOrDefault(p => p.Id == newProduct.Id);
                    if (existingProduct != null)
                    {
                        return false; // the product is already related to an inventory
                    }
                    inventory.Products.Add(newProduct);
                }
                ctx.SaveChanges();
                return true;
            }
        }
        public Domain.Model.Inventory GetById(int id, params Expression<Func<Domain.Model.Inventory, object>>[] includeProperties)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var query = ctx.Inventories.Where(i => i.Id == id);
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.FirstOrDefault();
            }
        }
        public IEnumerable<Domain.Model.Inventory> GetAll(Expression<Func<Domain.Model.Inventory, bool>> filter = null, Func<IQueryable<Domain.Model.Inventory>, IOrderedQueryable<Domain.Model.Inventory>> orderBy = null, string includeProperties = "")
        {
            using (var ctx = new InventoryStevDBContext())
            {
                IQueryable<Domain.Model.Inventory> query = ctx.Inventories;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                return query.ToList();
            }
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return await ctx.Products.FindAsync(productId);
            }
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Entry(product).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return true;
            }
        }
        public async Task<Product> GetProductById(int productId)
        {
            return await Task.FromResult(_dbContext.Products.FirstOrDefault(p => p.Id == productId));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            using (var dbContext = new InventoryStevDBContext())
            {
                return await Task.FromResult(dbContext.Products.ToList());
            }
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await GetProductById(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool InventoryExists(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                return ctx.Inventories.Any(i => i.Id == id);
            }
        }
    }
}