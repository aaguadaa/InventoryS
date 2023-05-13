using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class InventoryStevInitializer : DropCreateDatabaseIfModelChanges<InventoryStevDBContext>
    {
        protected override void Seed(InventoryStevDBContext context)
        {
            context.SaveChanges();
        }
    }
}