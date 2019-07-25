namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarColors", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CarColors", "CarCategory_Id", "dbo.CarCategories");
            DropIndex("dbo.CarColors", new[] { "CarId" });
            DropIndex("dbo.CarColors", new[] { "CarCategory_Id" });
            RenameColumn(table: "dbo.CarColors", name: "CarCategory_Id", newName: "CarCategoryId");
            AlterColumn("dbo.CarColors", "CarCategoryId", c => c.Long( defaultValue: 2));
            CreateIndex("dbo.CarColors", "CarCategoryId");
            AddForeignKey("dbo.CarColors", "CarCategoryId", "dbo.CarCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.CarColors", "CarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarColors", "CarId", c => c.Long(nullable: false));
            DropForeignKey("dbo.CarColors", "CarCategoryId", "dbo.CarCategories");
            DropIndex("dbo.CarColors", new[] { "CarCategoryId" });
            AlterColumn("dbo.CarColors", "CarCategoryId", c => c.Long());
            RenameColumn(table: "dbo.CarColors", name: "CarCategoryId", newName: "CarCategory_Id");
            CreateIndex("dbo.CarColors", "CarCategory_Id");
            CreateIndex("dbo.CarColors", "CarId");
            AddForeignKey("dbo.CarColors", "CarCategory_Id", "dbo.CarCategories", "Id");
            AddForeignKey("dbo.CarColors", "CarId", "dbo.Cars", "Id", cascadeDelete: true);
        }
    }
}
