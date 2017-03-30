namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddocid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compliances", "DocumentID", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compliances", "DocumentID");
        }
    }
}
