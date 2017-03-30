namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iscompliant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compliances", "IsCompliant", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compliances", "IsCompliant");
        }
    }
}
