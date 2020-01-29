namespace CarVendor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Currency");
            DropColumn("dbo.Orders", "TotalAmount");
        }
    }
}
