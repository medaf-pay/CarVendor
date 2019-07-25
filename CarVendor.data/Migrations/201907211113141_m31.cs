namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarColors", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CarColors", "CarCategory_Id", c => c.Long());
            CreateIndex("dbo.CarColors", "CarCategory_Id");
            AddForeignKey("dbo.CarColors", "CarCategory_Id", "dbo.CarCategories", "Id");
            DropColumn("dbo.CarCategories", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarCategories", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.CarColors", "CarCategory_Id", "dbo.CarCategories");
            DropIndex("dbo.CarColors", new[] { "CarCategory_Id" });
            DropColumn("dbo.CarColors", "CarCategory_Id");
            DropColumn("dbo.CarColors", "Price");
        }
    }
}
