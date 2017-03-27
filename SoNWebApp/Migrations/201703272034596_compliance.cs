namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compliance : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Documents", "Notes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Notes", c => c.String());
        }
    }
}
