namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentType : DbMigration
    {
        public override void Up()
        {
         
          
            AddColumn("dbo.OrderItems", "PaymenType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "PaymenType");
    
  
        }
    }
}
