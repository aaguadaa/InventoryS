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
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryStevDBContext _dbContext;
        public ProductRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            _dbContext.Set<Product>().Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }
        public bool Delete(int id)
        {
            var entity = _dbContext.Set<Product>().Find(id);

            if (entity == null)
                return false;

            _dbContext.Set<Product>().Remove(entity);
            _dbContext.SaveChanges();

            return true;
        }
        public Product Get(int id)
        {
            return _dbContext.Set<Product>().Find(id);
        }
        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Product> query = _dbContext.Set<Product>();

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
        public Product GetById(int id, params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _dbContext.Set<Product>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(p => p.Id == id);
        }
        public bool Update(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            var existingEntity = _dbContext.Set<Product>().Find(entity.Id);

            if (existingEntity == null)
                return false;

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
