namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _event : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "start_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "end_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Description", c => c.String());
            DropColumn("dbo.Events", "StartDate");
            DropColumn("dbo.Events", "EndDate");
            DropColumn("dbo.Events", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Name", c => c.String());
            AddColumn("dbo.Events", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "end_date");
            DropColumn("dbo.Events", "start_date");
        }
    }
}
