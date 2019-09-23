namespace CarVendor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v000 : DbMigration
    {
        public override void Up()
        {
           
            
          
            
           
            
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "userId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "TypeId", "dbo.CarFamilies");
            DropForeignKey("dbo.CarCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CarColors", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.CarImages", "CarColorId", "dbo.CarColors");
            DropForeignKey("dbo.CarColors", "CarCategoryId", "dbo.CarCategories");
            DropForeignKey("dbo.CarCategories", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "userId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserAddresses", new[] { "AddressId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.CarImages", new[] { "CarColorId" });
            DropIndex("dbo.CarColors", new[] { "ColorId" });
            DropIndex("dbo.CarColors", new[] { "CarCategoryId" });
            DropIndex("dbo.CarCategories", new[] { "CategoryId" });
            DropIndex("dbo.CarCategories", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "CarId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Addresses");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.CarFamilies");
            DropTable("dbo.Categories");
            DropTable("dbo.Colors");
            DropTable("dbo.CarImages");
            DropTable("dbo.CarColors");
            DropTable("dbo.CarCategories");
            DropTable("dbo.Brands");
            DropTable("dbo.Cars");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
        }
    }
}
