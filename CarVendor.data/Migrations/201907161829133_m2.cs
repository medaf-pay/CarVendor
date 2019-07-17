namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerDeliveryDetails", "PaymethodType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerDeliveryDetails", "PaymethodType", c => c.Long());
        }
    }
}
