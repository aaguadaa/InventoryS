namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stev : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 255),
                        Date = c.DateTime(nullable: false),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ProductId = c.Int(nullable: false),
                        Account_Id = c.Int(),
                        Inventory_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Inventories", t => t.Inventory_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Account_Id)
                .Index(t => t.Inventory_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Categoria = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        UpdatedProduct_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.UpdatedProduct_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.UpdatedProduct_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        Inventory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.Inventory_Id)
                .Index(t => t.Inventory_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Checks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Inventories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Checks", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Checks", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "UpdatedProduct_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Checks", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "Account_Id" });
            DropIndex("dbo.Products", new[] { "Inventory_Id" });
            DropIndex("dbo.Inventories", new[] { "User_Id" });
            DropIndex("dbo.Inventories", new[] { "UpdatedProduct_Id" });
            DropIndex("dbo.Inventories", new[] { "Id" });
            DropIndex("dbo.Checks", new[] { "User_Id" });
            DropIndex("dbo.Checks", new[] { "Inventory_Id" });
            DropIndex("dbo.Checks", new[] { "Account_Id" });
            DropIndex("dbo.Checks", new[] { "ProductId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Inventories");
            DropTable("dbo.Checks");
            DropTable("dbo.Accounts");
        }
    }
}
