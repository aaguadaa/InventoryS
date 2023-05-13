using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IProductService

    {
        int Add(Product product);
        //Read
        Product Get(int id);
        //Update
        bool Update(Product product);
        //Delete
        bool Delete(int product);
    }
}