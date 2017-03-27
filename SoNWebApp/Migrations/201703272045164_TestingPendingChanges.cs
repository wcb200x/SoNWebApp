namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingPendingChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compliances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StudentID = c.Int(nullable: false),
                        DocumentID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Documents", t => t.DocumentID, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: false)
                .Index(t => t.StudentID)
                .Index(t => t.DocumentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compliances", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Compliances", new[] { "DocumentID" });
            DropIndex("dbo.Compliances", new[] { "StudentID" });
            DropTable("dbo.Compliances");
        }
    }
}
