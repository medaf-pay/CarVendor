namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardInfoes", "Id", "dbo.CustomerDeliveryDetails");
            DropForeignKey("dbo.Orders", "DeliveryDetailsId", "dbo.CustomerDeliveryDetails");
            DropForeignKey("dbo.CustomerDeliveryDetails", "PaymethodTypeId", "dbo.PaymethodTypes");
            DropForeignKey("dbo.BankTransferInfoes", "Id", "dbo.CustomerDeliveryDetails");
            DropForeignKey("dbo.CorporateDetails", "Id", "dbo.CustomerDeliveryDetails");
            DropIndex("dbo.BankTransferInfoes", new[] { "Id" });
            DropIndex("dbo.CustomerDeliveryDetails", new[] { "PaymethodTypeId" });
            DropIndex("dbo.CardInfoes", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "DeliveryDetailsId" });
            DropPrimaryKey("dbo.BankTransferInfoes");
            DropPrimaryKey("dbo.CardInfoes");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Individually = c.Int(nullable: false),
                        FName = c.String(),
                        MName = c.String(),
                        LName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        NationalId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AddressId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MainAddress = c.String(),
                        DeliveryAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoldId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Role_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CorporateDetails", "CorporateName", c => c.String());
            AddColumn("dbo.CorporateDetails", "CorporateSite", c => c.String());
            AlterColumn("dbo.BankTransferInfoes", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CardInfoes", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CorporateDetails", "RegistrationNo", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BankTransferInfoes", "Id");
            AddPrimaryKey("dbo.CardInfoes", "Id");
            AddForeignKey("dbo.CorporateDetails", "Id", "dbo.Users", "Id");
            DropColumn("dbo.Orders", "DeliveryDetailsId");
            DropColumn("dbo.CorporateDetails", "OrgnizationName");
            DropColumn("dbo.CorporateDetails", "OrgnizationSite");
            DropColumn("dbo.CorporateDetails", "IsDeleted");
            DropTable("dbo.CustomerDeliveryDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerDeliveryDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        Mobile = c.String(),
                        Individually = c.Int(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        DeliveryAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        PaymethodTypeId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CorporateDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CorporateDetails", "OrgnizationSite", c => c.String());
            AddColumn("dbo.CorporateDetails", "OrgnizationName", c => c.String());
            AddColumn("dbo.Orders", "DeliveryDetailsId", c => c.Long(nullable: false));
            DropForeignKey("dbo.CorporateDetails", "Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "AddressId", "dbo.Addresses");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserAddresses", new[] { "AddressId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropPrimaryKey("dbo.CardInfoes");
            DropPrimaryKey("dbo.BankTransferInfoes");
            AlterColumn("dbo.CorporateDetails", "RegistrationNo", c => c.String());
            AlterColumn("dbo.CardInfoes", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.BankTransferInfoes", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.CorporateDetails", "CorporateSite");
            DropColumn("dbo.CorporateDetails", "CorporateName");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Addresses");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Users");
            AddPrimaryKey("dbo.CardInfoes", "Id");
            AddPrimaryKey("dbo.BankTransferInfoes", "Id");
            CreateIndex("dbo.Orders", "DeliveryDetailsId");
            CreateIndex("dbo.CardInfoes", "Id");
            CreateIndex("dbo.CustomerDeliveryDetails", "PaymethodTypeId");
            CreateIndex("dbo.BankTransferInfoes", "Id");
            AddForeignKey("dbo.CorporateDetails", "Id", "dbo.CustomerDeliveryDetails", "Id");
            AddForeignKey("dbo.BankTransferInfoes", "Id", "dbo.CustomerDeliveryDetails", "Id");
            AddForeignKey("dbo.CustomerDeliveryDetails", "PaymethodTypeId", "dbo.PaymethodTypes", "Id");
            AddForeignKey("dbo.Orders", "DeliveryDetailsId", "dbo.CustomerDeliveryDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardInfoes", "Id", "dbo.CustomerDeliveryDetails", "Id");
        }
    }
}
