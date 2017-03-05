namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removelocationfromapp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UDApplications", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UDApplications", "Location", c => c.String());
        }
    }
}
