namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inventories", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Checks", "Inventory_Id", "dbo.Inventories");
            DropIndex("dbo.Inventories", new[] { "Id" });
            DropPrimaryKey("dbo.Inventories");
            AddColumn("dbo.Inventories", "AccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.Inventories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Inventories", "Id");
            CreateIndex("dbo.Accounts", "InventoryId");
            CreateIndex("dbo.Inventories", "AccountId");
            AddForeignKey("dbo.Accounts", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Inventories", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories", "Id");
            AddForeignKey("dbo.Checks", "Inventory_Id", "dbo.Inventories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checks", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Inventories", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "InventoryId" });
            DropPrimaryKey("dbo.Inventories");
            AlterColumn("dbo.Inventories", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Inventories", "AccountId");
            AddPrimaryKey("dbo.Inventories", "Id");
            CreateIndex("dbo.Inventories", "Id");
            AddForeignKey("dbo.Checks", "Inventory_Id", "dbo.Inventories", "Id");
            AddForeignKey("dbo.Products", "Inventory_Id", "dbo.Inventories", "Id");
            AddForeignKey("dbo.Inventories", "Id", "dbo.Accounts", "Id");
        }
    }
}
