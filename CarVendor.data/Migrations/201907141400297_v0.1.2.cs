namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Category", c => c.String());
            AddColumn("dbo.Users", "Individually", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "OrgnizationName", c => c.String());
            AddColumn("dbo.Users", "OrgnizationSite", c => c.String());
            AddColumn("dbo.Users", "RegistrationNo", c => c.String());
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "MiddleName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "Address1", c => c.String());
            AddColumn("dbo.Users", "Address2", c => c.String());
            AddColumn("dbo.Users", "Country", c => c.String());
            AddColumn("dbo.Users", "City", c => c.String());
            AddColumn("dbo.Users", "State", c => c.String());
            AddColumn("dbo.Users", "Zip", c => c.String());
            AlterColumn("dbo.Brands", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Cars", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarColors", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarImages", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Colors", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OrderItems", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.DeliveryDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.DeliveryDetails", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Orders", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.OrderItems", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Categories", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Colors", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.CarImages", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.CarColors", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Cars", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.CarCategories", "IsDeleted", c => c.Boolean());
            AlterColumn("dbo.Brands", "IsDeleted", c => c.Boolean());
            DropColumn("dbo.Users", "Zip");
            DropColumn("dbo.Users", "State");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "Address2");
            DropColumn("dbo.Users", "Address1");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "MiddleName");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "RegistrationNo");
            DropColumn("dbo.Users", "OrgnizationSite");
            DropColumn("dbo.Users", "OrgnizationName");
            DropColumn("dbo.Users", "Individually");
            DropColumn("dbo.OrderItems", "Category");
        }
    }
}
