namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anunts", "Role_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Anunts", "Role_Id");
            AddForeignKey("dbo.Anunts", "Role_Id", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anunts", "Role_Id", "dbo.AspNetRoles");
            DropIndex("dbo.Anunts", new[] { "Role_Id" });
            DropColumn("dbo.Anunts", "Role_Id");
        }
    }
}
