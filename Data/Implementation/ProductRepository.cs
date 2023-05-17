using Data.Contracts;
using Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryStevDBContext _dbContext;

        public ProductRepository(InventoryStevDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _dbContext.Products.Where(p => p.Status == "Disponible").ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetUnavailableProductsAsync()
        {
            return await _dbContext.Products.Where(p => p.Status == "No Disponible").ToListAsync();
        }
    }
}