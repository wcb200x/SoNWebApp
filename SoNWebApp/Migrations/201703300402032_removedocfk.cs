namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedocfk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Compliances", new[] { "DocumentID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Compliances", "DocumentID");
            AddForeignKey("dbo.Compliances", "DocumentID", "dbo.Documents", "Id");
        }
    }
}
