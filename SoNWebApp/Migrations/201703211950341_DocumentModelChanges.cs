namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "DocumentType", c => c.String());
            AddColumn("dbo.Documents", "Notes", c => c.String());
            AddColumn("dbo.Documents", "ComplianceStatus", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "ComplianceStatus");
            DropColumn("dbo.Documents", "Notes");
            DropColumn("dbo.Documents", "DocumentType");
            DropColumn("dbo.Documents", "ExpirationDate");
        }
    }
}
