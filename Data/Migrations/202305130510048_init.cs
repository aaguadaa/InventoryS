namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        account_Id = c.Int(),
                        inventory_Id = c.Int(),
                        productUpdt_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.account_Id)
                .ForeignKey("dbo.Inventories", t => t.inventory_Id)
                .ForeignKey("dbo.Products", t => t.productUpdt_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.account_Id)
                .Index(t => t.inventory_Id)
                .Index(t => t.productUpdt_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        account_Id = c.Int(),
                        productUpdt_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.account_Id)
                .ForeignKey("dbo.Products", t => t.productUpdt_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.account_Id)
                .Index(t => t.productUpdt_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                        Descripcion = c.String(),
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
                        Name = c.String(),
                        Password = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Inventories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Checks", "productUpdt_Id", "dbo.Products");
            DropForeignKey("dbo.Checks", "inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "productUpdt_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Checks", "account_Id", "dbo.Accounts");
            DropIndex("dbo.Products", new[] { "Inventory_Id" });
            DropIndex("dbo.Inventories", new[] { "User_Id" });
            DropIndex("dbo.Inventories", new[] { "productUpdt_Id" });
            DropIndex("dbo.Inventories", new[] { "account_Id" });
            DropIndex("dbo.Checks", new[] { "User_Id" });
            DropIndex("dbo.Checks", new[] { "productUpdt_Id" });
            DropIndex("dbo.Checks", new[] { "inventory_Id" });
            DropIndex("dbo.Checks", new[] { "account_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Inventories");
            DropTable("dbo.Checks");
            DropTable("dbo.Accounts");
        }
    }
}
