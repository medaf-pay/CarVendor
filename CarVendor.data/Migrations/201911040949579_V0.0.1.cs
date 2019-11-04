namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromCurrencyId = c.Long(nullable: false),
                        ToCurrencyId = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        Plus = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Currency_Id = c.Long(),
                        Currency_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id1)
                .ForeignKey("dbo.Currencies", t => t.FromCurrencyId)
                .ForeignKey("dbo.Currencies", t => t.ToCurrencyId)
                .Index(t => t.FromCurrencyId)
                .Index(t => t.ToCurrencyId)
                .Index(t => t.Currency_Id)
                .Index(t => t.Currency_Id1);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CarColors", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversions", "ToCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "FromCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "Currency_Id1", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "Currency_Id", "dbo.Currencies");
            DropIndex("dbo.Conversions", new[] { "Currency_Id1" });
            DropIndex("dbo.Conversions", new[] { "Currency_Id" });
            DropIndex("dbo.Conversions", new[] { "ToCurrencyId" });
            DropIndex("dbo.Conversions", new[] { "FromCurrencyId" });
            DropColumn("dbo.CarColors", "Discount");
            DropTable("dbo.Currencies");
            DropTable("dbo.Conversions");
        }
    }
}
