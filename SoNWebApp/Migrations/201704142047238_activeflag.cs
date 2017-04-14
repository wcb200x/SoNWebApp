namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Active");
        }
    }
}
