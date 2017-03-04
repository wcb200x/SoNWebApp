namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountcontrollerforregister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UDApplications", "Program1", c => c.String());
            AddColumn("dbo.UDApplications", "Program2", c => c.String());
            AddColumn("dbo.UDApplications", "Program3", c => c.String());
            AddColumn("dbo.UDApplications", "ConfirmLegal", c => c.String());
            DropColumn("dbo.UDApplications", "Legal7");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UDApplications", "Legal7", c => c.String());
            DropColumn("dbo.UDApplications", "ConfirmLegal");
            DropColumn("dbo.UDApplications", "Program3");
            DropColumn("dbo.UDApplications", "Program2");
            DropColumn("dbo.UDApplications", "Program1");
        }
    }
}
