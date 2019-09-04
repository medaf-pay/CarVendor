namespace CarVendor.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            RenameColumn(table: "dbo.UserRoles", name: "Role_Id", newName: "RoleId");
            AlterColumn("dbo.UserRoles", "RoleId", c => c.Long(nullable: false));
            CreateIndex("dbo.UserRoles", "RoleId");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            DropColumn("dbo.UserRoles", "RoldId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "RoldId", c => c.Long(nullable: false));
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            AlterColumn("dbo.UserRoles", "RoleId", c => c.Long());
            RenameColumn(table: "dbo.UserRoles", name: "RoleId", newName: "Role_Id");
            CreateIndex("dbo.UserRoles", "Role_Id");
            AddForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles", "Id");
        }
    }
}
