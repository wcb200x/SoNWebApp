namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Compliances", "Name", c => c.String());
            AlterColumn("dbo.Compliances", "DocumentID", c => c.Int(nullable: true));
            AlterColumn("dbo.Compliances", "ExpirationDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Compliances", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Compliances", "DocumentID", c => c.Int());
            AlterColumn("dbo.Compliances", "ExpirationDate", c => c.DateTime());
        }
    }
}
