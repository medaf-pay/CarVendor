namespace CarVendor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CarColor_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarColors", t => t.CarColor_Id)
                .Index(t => t.CarColor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "CarColor_Id", "dbo.CarColors");
            DropIndex("dbo.Videos", new[] { "CarColor_Id" });
            DropTable("dbo.Videos");
        }
    }
}
