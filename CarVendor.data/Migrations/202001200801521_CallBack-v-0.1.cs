namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallBackv01 : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.PaymentCallBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResopnceObject = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentCallBacks");
      
        }
    }
}
