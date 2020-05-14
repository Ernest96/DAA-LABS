namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anuntv2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Anunts", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Anunts", "Created", c => c.DateTime());
        }
    }
}
