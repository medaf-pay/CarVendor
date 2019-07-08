namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarColors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CarImages", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Colors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeliveryDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.DeliveryDetails", "IsDeleted");
            DropColumn("dbo.Orders", "IsDeleted");
            DropColumn("dbo.OrderItems", "IsDeleted");
            DropColumn("dbo.Colors", "IsDeleted");
            DropColumn("dbo.CarImages", "IsDeleted");
            DropColumn("dbo.CarColors", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.CarCategories", "IsDeleted");
            DropColumn("dbo.Cars", "IsDeleted");
            DropColumn("dbo.Brands", "IsDeleted");
        }
    }
}
