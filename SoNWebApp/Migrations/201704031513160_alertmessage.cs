namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertmessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alerts", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alerts", "Message");
        }
    }
}
