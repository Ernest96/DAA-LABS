namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnuntId = c.Int(nullable: false),
                        Author = c.String(),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anunts", t => t.AnuntId, cascadeDelete: true)
                .Index(t => t.AnuntId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "AnuntId", "dbo.Anunts");
            DropIndex("dbo.Comments", new[] { "AnuntId" });
            DropTable("dbo.Comments");
        }
    }
}
