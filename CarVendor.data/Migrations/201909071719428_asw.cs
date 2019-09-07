namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankTransferInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BName = c.String(),
                        BBranch = c.String(),
                        PaymentDate = c.String(),
                        TransferNo = c.String(),
                        InputReferenceNo = c.String(),
                        ACH = c.String(),
                        Memo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                        TypeId = c.Int(nullable: false),
                        BrandId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.CarFamilies", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.CarFamilies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarColors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarCategoryId = c.Long(nullable: false),
                        ColorId = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarCategories", t => t.CarCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .Index(t => t.CarCategoryId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.CarImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageURL = c.String(),
                        CarColorId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CardNumber = c.String(),
                        ExpiryDateMonth = c.String(),
                        ExpiryDateYear = c.String(),
                        CVCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CorporateDetails",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CorporateName = c.String(),
                        CorporateSite = c.String(),
                        RegistrationNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
  
          
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        Category = c.String(),
                        CarId = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                        OrderDate = c.DateTime(nullable: false),
                        OrderNumber = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymethodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CorporateDetails", "Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.CarCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CarColors", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.CarImages", "CarColorId", "dbo.CarColors");
            DropForeignKey("dbo.CarColors", "CarCategoryId", "dbo.CarCategories");
            DropForeignKey("dbo.Cars", "TypeId", "dbo.CarFamilies");
            DropForeignKey("dbo.CarCategories", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "CarId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserAddresses", new[] { "AddressId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.CorporateDetails", new[] { "Id" });
            DropIndex("dbo.CarImages", new[] { "CarColorId" });
            DropIndex("dbo.CarColors", new[] { "ColorId" });
            DropIndex("dbo.CarColors", new[] { "CarCategoryId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropIndex("dbo.CarCategories", new[] { "CategoryId" });
            DropIndex("dbo.CarCategories", new[] { "CarId" });
            DropTable("dbo.PaymethodTypes");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Addresses");
         
       
            DropTable("dbo.CorporateDetails");
            DropTable("dbo.CardInfoes");
            DropTable("dbo.Categories");
            DropTable("dbo.Colors");
            DropTable("dbo.CarImages");
            DropTable("dbo.CarColors");
            DropTable("dbo.CarFamilies");
            DropTable("dbo.Cars");
            DropTable("dbo.CarCategories");
            DropTable("dbo.Brands");
            DropTable("dbo.BankTransferInfoes");
        }
    }
}
