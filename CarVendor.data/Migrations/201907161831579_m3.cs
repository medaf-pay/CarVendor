namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymethodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CustomerDeliveryDetails", "PaymethodTypeId", c => c.Int());
            CreateIndex("dbo.CustomerDeliveryDetails", "PaymethodTypeId");
            AddForeignKey("dbo.CustomerDeliveryDetails", "PaymethodTypeId", "dbo.PaymethodTypes", "Id");
            DropColumn("dbo.CustomerDeliveryDetails", "PaymethodType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerDeliveryDetails", "PaymethodType", c => c.Int());
            DropForeignKey("dbo.CustomerDeliveryDetails", "PaymethodTypeId", "dbo.PaymethodTypes");
            DropIndex("dbo.CustomerDeliveryDetails", new[] { "PaymethodTypeId" });
            DropColumn("dbo.CustomerDeliveryDetails", "PaymethodTypeId");
            DropTable("dbo.PaymethodTypes");
        }
    }
}
