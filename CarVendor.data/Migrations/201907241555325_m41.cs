namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m41 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarFamilies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "TypeId", c => c.Int(nullable: true));
            AddColumn("dbo.CarColors", "Quantity", c => c.Int(defaultValue:0));
            CreateIndex("dbo.Cars", "TypeId");
            AddForeignKey("dbo.Cars", "TypeId", "dbo.CarFamilies", "Id", cascadeDelete: true);
        
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Type", c => c.Int(nullable: true));
            DropForeignKey("dbo.Cars", "TypeId", "dbo.CarFamilies");
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropColumn("dbo.CarColors", "Quantity");
            DropColumn("dbo.Cars", "TypeId");
            DropTable("dbo.CarFamilies");
        }
    }
}
