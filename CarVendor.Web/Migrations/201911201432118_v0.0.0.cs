namespace CarVendor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carosels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Description = c.String(),
                        Price = c.String(),
                        Mileage = c.String(),
                        VolumeCapacity = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Carosels");
        }
    }
}
