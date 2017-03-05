namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentForeignKey2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Documents", "StudentID");
            AddForeignKey("dbo.Documents", "StudentID", "dbo.Students", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "StudentID", "dbo.Students");
            DropIndex("dbo.Documents", new[] { "StudentID" });
        }
    }
}
