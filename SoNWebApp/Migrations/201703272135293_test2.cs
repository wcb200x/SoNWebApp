namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Compliances", new[] { "DocumentID" });
            AlterColumn("dbo.Compliances", "DocumentID", c => c.Int());
            AlterColumn("dbo.Compliances", "ExpirationDate", c => c.DateTime());
            CreateIndex("dbo.Compliances", "DocumentID");
            AddForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Compliances", new[] { "DocumentID" });
            AlterColumn("dbo.Compliances", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Compliances", "DocumentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Compliances", "DocumentID");
            AddForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents", "Id", cascadeDelete: true);
        }
    }
}
