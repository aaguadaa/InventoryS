using Business.Contracts;
using Data.Contracts;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public int Add(Product product)
        {
            if (product.Id <= 0) return 0;
            if (string.IsNullOrEmpty(product.Categoria)) return 0;
            if (string.IsNullOrEmpty(product.Descripcion)) return 0;
            if (string.IsNullOrEmpty(product.Status)) return 0;
            return _productRepo.Add(product);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;
            return (_productRepo.Delete(id));
        }

        public Product Get(int id)
        {
            Product product = _productRepo.Get(id);
            return product;
        }

        public bool Update(Product product)
        {
            if (product.Id <= 0) return false;
            if (string.IsNullOrEmpty(product.Categoria)) return false;
            if (string.IsNullOrEmpty(product.Descripcion)) return false;
            if (string.IsNullOrEmpty(product.Status)) return false;
            return _productRepo.Update(product);
        }
    }
}