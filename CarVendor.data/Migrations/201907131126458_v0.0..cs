namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        Condition = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        BrandId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.CarColors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarId = c.Long(nullable: false),
                        ColorId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.CarImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageURL = c.String(),
                        CarColorId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarColors", t => t.CarColorId, cascadeDelete: true)
                .Index(t => t.CarColorId);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        CarId = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeliveryDetailsId = c.Long(nullable: false),
                        OwnerId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryDetails", t => t.DeliveryDetailsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.DeliveryDetailsId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.DeliveryDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Address = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Town = c.String(),
                        ContactPerson = c.String(),
                        ContactNumber = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Mobile = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DeliveryDetailsId", "dbo.DeliveryDetails");
            DropForeignKey("dbo.OrderItems", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CarCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CarColors", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.CarImages", "CarColorId", "dbo.CarColors");
            DropForeignKey("dbo.CarColors", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CarCategories", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.Orders", new[] { "OwnerId" });
            DropIndex("dbo.Orders", new[] { "DeliveryDetailsId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "CarId" });
            DropIndex("dbo.CarImages", new[] { "CarColorId" });
            DropIndex("dbo.CarColors", new[] { "ColorId" });
            DropIndex("dbo.CarColors", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropIndex("dbo.CarCategories", new[] { "CategoryId" });
            DropIndex("dbo.CarCategories", new[] { "CarId" });
            DropTable("dbo.Users");
            DropTable("dbo.DeliveryDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Categories");
            DropTable("dbo.Colors");
            DropTable("dbo.CarImages");
            DropTable("dbo.CarColors");
            DropTable("dbo.Cars");
            DropTable("dbo.CarCategories");
            DropTable("dbo.Brands");
        }
    }
}
