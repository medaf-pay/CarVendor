namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallBackv02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.String());
            AddColumn("dbo.OrderItems", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderItems", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BankTransferInfoes", "OrderId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankTransferInfoes", "OrderId");
            DropColumn("dbo.OrderItems", "TotalPrice");
            DropColumn("dbo.OrderItems", "UnitPrice");
            DropColumn("dbo.Orders", "Status");
        }
    }
}
