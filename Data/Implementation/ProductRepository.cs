using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public int Add(Product entity)
        {
            if (entity == null) return 0;
            using (var ctx = new InventoryStevDBContext())
            {
                ctx.Products.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }

        }
        public bool Delete(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                Product p = ctx.Products.SingleOrDefault(t => t.Id == id);
                if (p == null) return false;
                ctx.Products.Remove(p);
                ctx.SaveChanges();
                return true;
            }
        }

        public Product Get(int id)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                Product product = ctx.Products.SingleOrDefault(t => t.Id == id);
                if (product == null) return null;
                return product;
            }
        }

        public bool Update(Product entity)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                Product product = ctx.Products.SingleOrDefault(t => t.Id == entity.Id); //busca el objeto Task con el id correspondiente
                product.Categoria = entity.Categoria;
                product.Descripcion = entity.Descripcion;
                product.Status = entity.Status;
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
