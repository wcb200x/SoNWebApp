namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mappingclass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Compliances", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Compliances", "Name", c => c.String(nullable: false));
        }
    }
}
