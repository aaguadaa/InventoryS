using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Model;

namespace Data
{
    public class InventoryStevDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Domain.Model.Inventory> Inventories { get; set; }

        public InventoryStevDBContext() : base("InventoryStev")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Inventory y Account
            modelBuilder.Entity<Domain.Model.Inventory>()
                        .HasRequired(i => i.Account)
                        .WithMany(a => a.Inventories)
                        .HasForeignKey(i => i.AccountId);

            base.OnModelCreating(modelBuilder);
        }
    }
}