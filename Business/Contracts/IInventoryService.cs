using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IInventoryService

    {
        int Add(Domain.Model.Inventory inventory);
        //Read
        Domain.Model.Inventory Get(int id);
        //Update
        bool Update(Domain.Model.Inventory inventory);
        //Delete
        bool Delete(int id);
    }
}